namespace Domain.Entities;

public class PreparationOrderDomain
{
    public int Id  { get; }
    public string ComoEnvia { get; private set; } = null!;
    public string Detalles { get; private set; } = null!;
    public bool? Estado { get; private set; }

    public PreparationOrderDomain(int id,string comoEnvia, string detalles)
    {
        var errores = new List<string>();

        if (string.IsNullOrWhiteSpace(comoEnvia))
            errores.Add("ComoEnvia no puede estar vacío");

        if (string.IsNullOrWhiteSpace(detalles))
            errores.Add("Detalles no puede estar vacío");

        // Estado puede ser null, así que no se valida aquí

        if (errores.Any())
            throw new ArgumentException("Error en los datos: " + string.Join(", ", errores));
        Id = id;
        ComoEnvia = comoEnvia;
        Detalles = detalles;
          
    }

    public void CambiarEstado()
    {
        Estado = true;
    }
}