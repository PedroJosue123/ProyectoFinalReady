namespace Domain.Dtos;

public class SendProductDto
{

    

    public string NombreEmpresa { get; set; } = null!;

    public string RucEmpresa { get; set; } = null!;

    public string Asesor { get; set; } = null!;

    public string NumeroTelefonico { get; set; } = null!;

    public string DireccionEnvio { get; set; } = null!;

    public string DireccionRecojo { get; set; } = null!;

    public DateTime FechaLlegada { get; set; }

    public string NroGuia { get; set; } = null!;
}