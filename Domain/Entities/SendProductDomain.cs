namespace Domain.Entities

{
    public class SendProductDomain
    {
        public int IdSend { get; private set; }
        public string NombreEmpresa { get; set; } = null!;
        public string RucEmpresa { get; set; } = null!;
        public string Asesor { get; set; } = null!;
        public string NumeroTelefonico { get; set; } = null!;
        public string DireccionEnvio { get; set; } = null!;
        public string DireccionRecojo { get; set; } = null!;
        public DateTime FechaLlegada { get; set; }
        public string NroGuia { get; set; } = null!;
        
        public bool? Estado { get; private set; }
        
        

        public SendProductDomain(
            int idSend,
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
            var errores = new List<string>();

            if (string.IsNullOrWhiteSpace(nombreEmpresa)) errores.Add(nameof(nombreEmpresa));
            if (string.IsNullOrWhiteSpace(rucEmpresa)) errores.Add(nameof(rucEmpresa));
            if (string.IsNullOrWhiteSpace(asesor)) errores.Add(nameof(asesor));
            if (string.IsNullOrWhiteSpace(numeroTelefonico)) errores.Add(nameof(numeroTelefonico));
            if (string.IsNullOrWhiteSpace(direccionEnvio)) errores.Add(nameof(direccionEnvio));
            if (string.IsNullOrWhiteSpace(direccionRecojo)) errores.Add(nameof(direccionRecojo));
            if (fechaLlegada == default) errores.Add(nameof(fechaLlegada));
            if (string.IsNullOrWhiteSpace(nroGuia)) errores.Add(nameof(nroGuia));

            if (errores.Any())
                throw new ArgumentException("Los siguientes campos son obligatorios o inválidos: " + string.Join(", ", errores));
            IdSend = idSend;
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
}