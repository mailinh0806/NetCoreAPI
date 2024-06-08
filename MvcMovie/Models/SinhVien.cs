using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models;
public class SinhVien
{
    [Key]
    public string MaSV { get; set; }
    public string TenSV { get; set; }
    public string Tuoi { get; set; }
    public string Lop { get; set; }
}