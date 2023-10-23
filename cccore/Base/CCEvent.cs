namespace CcCore
{
    /// <summary>
    /// Describe los eventos que soporta CopyCopy Core
    /// para informar sobre lo que Windows Explorer esta por
    /// hacer
    /// </summary>
    public enum CCEvent:byte
    {
        /// <summary>
        /// Windows explorer va a Copiar archivos
        /// </summary>
        Copy = 0,

        /// <summary>
        /// Windows Explorer va a Mover archivos
        /// </summary>
        Move = 1,


        /// <summary>
        /// Windows Explorer va a Borrar archivos
        /// </summary>
        Delete = 2
    }
}
