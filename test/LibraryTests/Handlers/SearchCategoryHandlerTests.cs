using NUnit.Framework;
using Telegram.Bot.Types;

namespace Proyecto_Final
{
    /// <summary>
    /// Prueba de la clase <see cref="SearchCategoryHandler"/>.
    /// </summary>
    [TestFixture]
    public class SearchCategoryHandlerTests
    {   
        /// <summary>
        /// Para saber si un handler es capaz de procesar el mensaje; podemos ver el método de IHandle <see cref="IHandler.Handle"/>. Este nos indica que si un handler es capaz de procesar un mensaje nos retornará ese handler, en caso contrario retornará null. Siempre y cuando los handlers no retornen null al ejecutar el método "Handle", entonces podemos asegurar que el Handler es capaz de responder a su mensaje clave.
        /// </summary>
        [Test]
        public void SearchCategoryHandlerTest()
        {
            Message mensaje = new Message();
            mensaje.From = new User();
            mensaje.Text = "/buscar_categoria";
            mensaje.From.Id = 874234256;
            TelegramAdapter msj_adaptado = new TelegramAdapter(mensaje);

            Singleton<StatusManager>.Instance.AgregarEstadoUsuario(mensaje.From.Id.ToString(),"STATUS_IDLE");

            IHandler firstHandler = new SearchCategoryHandler(null);

            string response = string.Empty;

            Assert.IsTrue(firstHandler.Handle(msj_adaptado,out response) != null);
        }
    }
}