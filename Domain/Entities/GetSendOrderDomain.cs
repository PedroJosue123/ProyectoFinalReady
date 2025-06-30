namespace Domain.Entities;

public class GetSendOrderDomain
{
    public string NombreEmpresa { get; set; } = null!;
    public string RucEmpresa { get; set; } = null!;
    public string Asesor { get; set; } = null!;
    public string NumeroTelefonico { get; set; } = null!;
    public string DireccionEnvio { get; set; } = null!;
    public string DireccionRecojo { get; set; } = null!;
    public DateTime FechaLlegada { get; set; }
    public string NroGuia { get; set; } = null!;
        
        
    public GetSendOrderDomain(
          
        string nombreEmpresa,
        string rucEmpresa,
        string asesor,
        string numeroTelefonico,
        string direccionEnvio,
        string direccionRecojo,
        DateTime fechaLlegada,
        string nroGuia
    )
    {
        NombreEmpresa = nombreEmpresa;
        RucEmpresa = rucEmpresa;
        Asesor = asesor;
        NumeroTelefonico = numeroTelefonico;
        DireccionEnvio = direccionEnvio;
        DireccionRecojo = direccionRecojo;
        FechaLlegada = fechaLlegada;
        NroGuia = nroGuia;
           

    }

      
}