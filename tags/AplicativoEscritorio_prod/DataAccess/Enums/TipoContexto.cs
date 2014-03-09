using System.ComponentModel;
namespace AplicativoEscritorio.DataAccess.Enums
{
    public enum TipoContexto
    {
        [DescriptionAttribute("Global")]
        Global = 0,
        [DescriptionAttribute("Local")]
        Local = 1
    }
}
