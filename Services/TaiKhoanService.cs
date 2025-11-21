using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TechCenter.DTO;
using TechCenter.DTO.HocVien;
using TechCenter.Helper;
using TechCenter.Models;
using TechCenter.Services.Interface;
namespace TechCenter.Services
{
    public class TaiKhoanService : ITaiKhoanService
    {
        private readonly string _secret;
        private readonly AppDbContext _context;
        private readonly IHocVienService _hocVienService;
        public TaiKhoanService(AppDbContext context, IConfiguration configuration, IHocVienService hocVienService)
        {
            _secret = configuration.GetValue<string>("Jwt:SecretKey");
            _context = context;
            _hocVienService = hocVienService;
        }
       

        public async Task<object> CreateTaiKhoan(string tenDangNhap, string matKhau, string email, int vaiTro, string tenNguoiDung)
        {
            bool emailExists = await _context.Taikhoans.AnyAsync(t => t.Email == email && t.Tendangnhap == tenDangNhap);
            if (emailExists)
            {
                throw new Exception("Email hoặc tên đăng nhập đã tồn tại");
            }

            // Hash mật khẩu
            string passwordHash = Md5Helper.GetMd5Hash(matKhau);

            var taiKhoan = new Taikhoan
            {
                IdVaitro = vaiTro,
                Tendangnhap = tenDangNhap,
                Matkhauhash = passwordHash,
                Email = email,
                Ngaytao = DateOnly.FromDateTime(DateTime.Now),
                IsActive = true,
            };

            _context.Taikhoans.Add(taiKhoan);
            await _context.SaveChangesAsync();

            int idTaiKhoan = taiKhoan.IdTaikhoan; // Lấy id vừa tạo

            // thêm thông tin học viên
            if (vaiTro == 2)
            {
               await _hocVienService.CreateHocVien(new HocVienDTO
                {
                    hoTenHv = tenNguoiDung,
                    idTaiKhoan = idTaiKhoan
                });
            }

            // Sinh token JWT
            var token = GenerateToken(taiKhoan);


            return new
            {
                Token = token,
                User = new
                {
                    idTaikhoan = idTaiKhoan,
                    tendangnhap = taiKhoan.Tendangnhap,
                    vaitro = vaiTro
                    
                }
            };
        }

        public async Task<object> Login(DangNhapDTO dangNhapDTO)
        {
            // Băm mật khẩu nhập vào từ người dùng
            string passwordHash = Md5Helper.GetMd5Hash(dangNhapDTO.Matkhau);

            // Tìm tài khoản khớp
            var taiKhoan = await _context.Taikhoans
                .FirstOrDefaultAsync(t => t.Tendangnhap == dangNhapDTO.Tendangnhap
                                      && t.Matkhauhash == passwordHash);


            //var tenNguoiDung = await _context.Hocviens
            //    .Where(hv => hv.IdTaikhoan == taiKhoan.IdTaikhoan)
            //    .Select(hv => hv.ten)
            //    .FirstOrDefaultAsync();

            if (taiKhoan == null)
            {
                throw new Exception("Tên đăng nhập hoặc mật khẩu không đúng");
            }

            // Lấy tên vai trò
            

            // Sinh token JWT
            var token = GenerateToken(taiKhoan);

            // Trả về token + thông tin user
            return new
            {
                Token = token,
                User = new
                {
                    IdTaikhoan = taiKhoan.IdTaikhoan,
                    Tendangnhap = taiKhoan.Tendangnhap,
                    //Email = taiKhoan.Email,
                    //Sodienthoai = taiKhoan.Sodienthoai,
                }
            };
        }



        public string GenerateToken(Taikhoan taikhoan, string currentToken = null)
        {
            try
            {
                
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_secret);

                if (!string.IsNullOrEmpty(currentToken))
                {
                    var securityToken = tokenHandler.ReadToken(currentToken) as JwtSecurityToken;
                    if (securityToken == null)
                    {
                        throw new SecurityTokenException("Token không hợp lệ");
                    }

                    var expirationDate = securityToken.ValidTo;

                    if (expirationDate > DateTime.UtcNow)
                    {
                        return currentToken;
                    }
                }

               string  tenVt = _context.Vaitros
                    .Where(vt => vt.IdVaitro == taikhoan.IdVaitro)
                    .Select(vt => vt.Tenvaitro)
                    .FirstOrDefault();

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.NameIdentifier, taikhoan.IdTaikhoan.ToString()),
                    new Claim(ClaimTypes.Name, taikhoan.Tendangnhap),
                    // Thêm quyền (role) của người dùng
                    new Claim(ClaimTypes.Role, tenVt)
                    }),
                    //Expires = DateTime.UtcNow.AddDays(1),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                // Log ngoại lệ (ví dụ: sử dụng framework logging)
                throw new ApplicationException("Đã xảy ra lỗi khi tạo token.", ex);
            }
        }

        public string RefreshTokenNguoiDung(Taikhoan taikhoan, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (securityToken == null)
                {
                    throw new SecurityTokenException("Token không hợp lệ");
                }

                var expirationDate = securityToken.ValidTo;
                if (expirationDate < DateTime.UtcNow)
                {
                    return GenerateToken(taikhoan);
                }

                return token;
            }
            catch (Exception ex)
            {
                // Log ngoại lệ (ví dụ: sử dụng framework logging)
                throw new ApplicationException("Đã xảy ra lỗi khi làm mới token.", ex);
            }
        }



    }
       
}
