namespace Bai_01.Models;

public partial class ChiTieu
{
    public string NgayChiDisplay => NgayChi.ToString("dd/MM/yyyy");

    public string SoTienFormatted => SoTien.ToString("N0");
}
