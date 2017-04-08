using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if WINDOWS_UWP
using System.Threading.Tasks;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
#else
using WebSocketSharp;
using WebSocketSharp.Net;
#endif

public class WebSocketClient {
    public event EventHandler<WebSocketEventArgs> OnMessaged;
#if WINDOWS_UWP
    MessageWebSocket websocket;
    string uri;
#else
    WebSocket websocket;
#endif

    public WebSocketClient(string uri) {
#if WINDOWS_UWP
        websocket = new MessageWebSocket();
        websocket.Control.MessageType = SocketMessageType.Utf8;
        websocket.MessageReceived += Websocket_MessageReceived;
        this.uri = uri;
#else
        websocket = new WebSocket(uri);
        Debug.Log(websocket.Url);
        websocket.OnMessage += Websocket_OnMessage;
#endif
    }

#if WINDOWS_UWP
    void Websocket_MessageReceived(MessageWebSocket sender, MessageWebSocketMessageReceivedEventArgs args) {
        if (OnMessaged != null) {
            var reader = args.GetDataReader();
            reader.UnicodeEncoding = Windows.Storage.Streams.UnicodeEncoding.Utf8;
            OnMessaged(sender, new WebSocketEventArgs(reader.ReadString(reader.UnconsumedBufferLength)));
        }
    }
#else
    void Websocket_OnMessage(object sender, MessageEventArgs e) {
        if (OnMessaged != null) {
            OnMessaged(sender, new WebSocketEventArgs(e.Data));
        }
    }
#endif


    public void Connect() {
#if WINDOWS_UWP
        var task = Task.Run(async () => {
            await websocket.ConnectAsync(new Uri(uri));
        });
        task.Wait();
#else
        websocket.Connect();
        Debug.Log(websocket.IsAlive);
#endif
    }

    public void Disconnect() {
#if WINDOWS_UWP
        websocket.Close(1000, "Disconnect function is called.");
#else
        websocket.Close();
        websocket = null;
#endif
    }

    public void Send(string message) {
#if WINDOWS_UWP
        var task = Task.Run(async () => {
            var writer = new DataWriter(websocket.OutputStream);
            writer.WriteString(message);
            await writer.StoreAsync();
        });
        task.Wait();
#else
        websocket.Send(message);
#endif
    }

    public class WebSocketEventArgs : EventArgs {
        public string Message { get; private set; }
        public WebSocketEventArgs(string message) : base() {
            this.Message = message;
        }
    }
}
