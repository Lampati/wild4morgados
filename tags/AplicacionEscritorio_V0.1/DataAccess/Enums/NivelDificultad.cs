namespace AplicativoEscritorio.DataAccess.Enums
{
    public enum NivelDificultad
    {
        /// <summary>
        /// Modo más sencillo de todos. Para los que recién comienzan.
        /// </summary>
        Principiante = 0,
        /// <summary>
        /// Modo adecuado para aquellos que tienen un poco más de experiencia.
        /// </summary>
        Normal = 1,
        /// <summary>
        /// Modo dificil, requiere de gran experiencia en la resolución de ejercicios a través de GarGar.
        /// </summary>
        Experto = 2,
        /// <summary>
        /// No creo que nadie quiera resolver este tipo de ejercicios... Por ejemplo: Escribir un algoritmo que permita
        /// determinar si una pieza de código finalizará o no y cuánto tiempo tardará en ejecutarse. Sólo para osados.
        /// </summary>
        DIOS = 3
    }
}
