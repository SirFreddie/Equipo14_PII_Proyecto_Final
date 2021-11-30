﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Proyecto_Final
{
    /// <summary>
    /// Un programa que implementa un bot de Telegram.
    /// </summary>
    public static class Program
    {
        // La instancia del bot.
        private static TelegramBotClient Bot;

        // El token provisto por Telegram al crear el bot.
        //
        // *Importante*:
        // Para probar este ejemplo, crea un bot nuevo y eeemplaza este token por el de tu bot.

        private static string Token = "2135269832:AAFLneVWbn_FduSGcpKCCIfiIJyDzoH3oTk";

        private static IHandler firstHandler;

        /// <summary>
        /// Punto de entrada al programa.
        /// </summary>
        public static void Main()
        {
            Bot = new TelegramBotClient(Token);

            firstHandler =
                new StartHandler(
                new ExitHandler(
                new HelloHandler(
                new GoodByeHandler(
                new InviteHandler(
                new RegisterHandler(
                new PublishHandler(
                new SearchCategoryHandler(
                new SearchKeyWordsHandler(
                new SearchZoneHandler(
                new MaterialsConsumedHandler(
                new AddKeyWordHandler(
                new AddAuthorizationHandler(
                new PeriodOfTimeHandler(
                new SearchRecurrencyHandler(
                new ShowInterestInOfferHandler(
                new EndOfferHandler(
                new CommandsHandler(
                new PhotoHandler(Bot, null)
            ))))))))))))))))));

            var cts = new CancellationTokenSource();

            // Comenzamos a escuchar mensajes. Esto se hace en otro hilo (en background). El primer método
            // HandleUpdateAsync es invocado por el bot cuando se recibe un mensaje. El segundo método HandleErrorAsync
            // es invocado cuando ocurre un error.
            Bot.StartReceiving(
                new DefaultUpdateHandler(HandleUpdateAsync, HandleErrorAsync),
                cts.Token
            );

            Console.WriteLine($"[PROGRAM] : BOT STARTED.");
            Singleton<Datos>.Instance.LoadData();

            // Esperamos a que el usuario aprete Enter en la consola para terminar el bot.
            Console.ReadLine();

            // Terminamos el bot.
            cts.Cancel();
        }

        /// <summary>
        /// Maneja las actualizaciones del bot (todo lo que llega), incluyendo mensajes, ediciones de mensajes,
        /// respuestas a botones, etc. En este ejemplo sólo manejamos mensajes de texto.
        /// </summary>
        public static async Task HandleUpdateAsync(Update update, CancellationToken cancellationToken)
        {
            try
            {
                // Sólo respondemos a mensajes de texto
                if (update.Type == UpdateType.Message)
                {
                    await HandleMessageReceived(new TelegramAdapter(update.Message));
                }
            }
            catch(Exception e)
            {
                await HandleErrorAsync(e, cancellationToken);
            }
        }

        /// <summary>
        /// Maneja los mensajes que se envían al bot.
        /// Lo único que hacemos por ahora es escuchar 3 tipos de mensajes:
        /// - "hola": responde con texto
        /// - "chau": responde con texto
        /// - "foto": responde con una foto
        /// </summary>
        /// <param name="message">El mensaje recibido</param>
        /// <returns></returns>
        private static async Task HandleMessageReceived(IMessage message)
        {
            Console.WriteLine($"[PROGRAM] : < {message.UserId} > | Received a message from {message.FirstName} saying: {message.Text} | Chat: {message.ChatId} | {message.Date} | Status: {Singleton<StatusManager>.Instance.CheckStatus(message.UserId)}");

            string response = string.Empty;

            if (Singleton<StatusManager>.Instance.ListaEstadoUsuario().ContainsKey(message.UserId))
            {
                firstHandler.Handle(message, out response);
            }
            else 
            {
                Singleton<StatusManager>.Instance.AgregarEstadoUsuario(message.UserId, "STATUS_IDLE");
                firstHandler.Handle(message, out response);
            } 


            //Singleton<StatusManager>.Instance.PrintUserStatus();

            if (!string.IsNullOrEmpty(response))
            {
                await Bot.SendTextMessageAsync(message.ChatId, response);
            }
        }

        /// <summary>
        /// Manejo de excepciones. Por ahora simplemente la imprimimos en la consola.
        /// </summary>
        public static Task HandleErrorAsync(Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(exception.Message);
            return Task.CompletedTask;
        }
    }
}