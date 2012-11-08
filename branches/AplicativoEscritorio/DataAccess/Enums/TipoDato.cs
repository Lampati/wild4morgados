using System.ComponentModel;
namespace AplicativoEscritorio.DataAccess.Enums
{
    public enum TipoDato
    {
        [DescriptionAttribute("Texto")]
        Texto = 0,
        [DescriptionAttribute("Numero")]
        Numero = 1,
        [DescriptionAttribute("Booleano")]
        Booleano = 2,
        [DescriptionAttribute("Ninguno")]
        Ninguno = 3
    }
}
