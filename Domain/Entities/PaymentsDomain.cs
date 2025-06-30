namespace Domain.Entities;

public class PaymentsDomain
{
    public int IdPago { get; set; }

    public DateTime? FechaPago { get; set; }

    public bool? Estado { get; set; }

    public decimal? Monto { get; set; }
    
    

    // Constructor
    public PaymentsDomain(int idPago, DateTime? fechaPago, bool? estado, decimal? monto)
    {
        IdPago = idPago;
        FechaPago = fechaPago;
        Estado = estado;
        Monto = monto;
       
    }
}