// private bool isServerRunning = false;  // This flag tells us whether the server is currently running or not.

// private IEnumerator StartServerCoroutine()
// {
//     Debug.Log("Starting StartServerCoroutine");

//     if (isServerRunning)
//     {
//         // If the server is already running, log a warning and exit early.
//         Debug.LogWarning("Server is already running.");
//         yield break;  // This means stop here and don't go further.
//     }

//     try
//     {
//         isServerRunning = true;  // Set the flag to true, indicating that the server is now starting.

//         if (udpServer == null)
//         {
//             udpServer = new UdpClient(serverPort);  // If the UDP server is not already created, create it.
//             udpServer.EnableBroadcast = true;
//         }

//         // The rest of the code for initializing the server...

//         Debug.Log($"Broadcast messages sent to {networkBroadcastAddress}:{networkBroadcastPort}");
//     }
//     catch (SocketException ex)
//     {
//         if (ex.SocketErrorCode == SocketError.AddressAlreadyInUse)
//         {
//             // If there's an error because the port is already in use, log a warning.
//             Debug.LogWarning($"Port {serverPort} is already in use.");
//         }
//         else
//         {
//             // If there's any other error, log it as an error.
//             Debug.LogError($"Error starting server: {ex.Message}");
//         }
//     }
//     finally
//     {
//         isServerRunning = false;  // Set the flag back to false, indicating that the server has finished or encountered an error.
//     }

//     yield return null;  // Allow other things to happen before the next iteration (if this is part of a coroutine).
// }



// Explanation:

// Flag to Track Server State:

// isServerRunning is like a switch that tells us if the server is currently doing its thing.
// Checking if Server is Already Running:

// Before trying to start the server, we check if it's already running.
// If it is, we log a warning and stop trying to start it again.
// Setting the Flag and Starting the Server:

// If the server is not running, we set the flag to say, "Hey, the server is starting now!"
// We then go ahead and start the server.
// Handling Errors:

// If there's an error, like the server port being in use, we log a warning.
// If there's any other kind of error, we log it as a more serious error.
// Setting the Flag Back:

// Finally, whether the server started successfully or encountered an error, we set the flag back to say, "Okay, the server is done now."
// yield return null;:

// This line says, "Let's pause here for a moment and let other things happen before we do anything else." It helps keep everything running smoothly in the game.
// This way, we make sure the server is started and stopped in a good order, and we're careful not to start it multiple times at once. It's like making sure everyone takes turns using the same toy so that things don't get messy!









// private bool isServerRunning = false;
// private const string pipeName = "UnityToResponderPipe";
// private NamedPipeServerStream pipeServer;
// private StreamWriter pipeStreamWriter;

// private IEnumerator StartServerCoroutine()
// {
//     Debug.Log("Starting StartServerCoroutine");

//     if (isServerRunning)
//     {
//         Debug.LogWarning("Server is already running.");
//         yield break;
//     }

//     try
//     {
//         isServerRunning = true;

//         if (udpServer == null)
//         {
//             udpServer = new UdpClient(serverPort);
//             udpServer.EnableBroadcast = true;
//         }

//         // Start named pipe server for communication
//         StartPipeServer();

//         IPAddress networkBroadcastAddress = IPAddress.Parse("192.168.0.255");
//         int networkBroadcastPort = 12345;

//         string broadcastMessage = "DISCOVER";
//         byte[] bytes = Encoding.ASCII.GetBytes(broadcastMessage);
//         udpServer.Send(bytes, bytes.Length, new IPEndPoint(networkBroadcastAddress, networkBroadcastPort));

//         // Send the broadcast message also through the named pipe
//         SendMessageThroughPipe(broadcastMessage);

//         Debug.Log($"Broadcast messages sent to {networkBroadcastAddress}:{networkBroadcastPort}");
//     }
//     catch (SocketException ex)
//     {
//         if (ex.SocketErrorCode == SocketError.AddressAlreadyInUse)
//         {
//             Debug.LogWarning($"Port {serverPort} is already in use.");
//         }
//         else
//         {
//             Debug.LogError($"Error starting server: {ex.Message}");
//         }
//     }
//     finally
//     {
//         isServerRunning = false;
//         StopPipeServer();
//     }

//     yield return null;
// }

// private void StartPipeServer()
// {
//     try
//     {
//         // Start the named pipe server
//         pipeServer = new NamedPipeServerStream(pipeName, PipeDirection.Out);
//         pipeStreamWriter = new StreamWriter(pipeServer);
//         pipeServer.WaitForConnection();  // Wait for a client to connect to the named pipe
//     }
//     catch (Exception ex)
//     {
//         Debug.LogError($"Error starting pipe server: {ex.Message}");
//     }
// }

// private void StopPipeServer()
// {
//     try
//     {
//         // Stop the named pipe server
//         if (pipeStreamWriter != null)
//         {
//             pipeStreamWriter.Close();
//         }

//         if (pipeServer != null && pipeServer.IsConnected)
//         {
//             pipeServer.Disconnect();
//         }

//         pipeServer.Close();
//     }
//     catch (Exception ex)
//     {
//         Debug.LogError($"Error stopping pipe server: {ex.Message}");
//     }
// }

// private void SendMessageThroughPipe(string message)
// {
//     try
//     {
//         // Send a message through the named pipe
//         if (pipeStreamWriter != null)
//         {
//             // Check if the pipe is connected before writing
//             if (pipeServer != null && pipeServer.IsConnected)
//             {
//                 pipeStreamWriter.WriteLine(message);
//                 pipeStreamWriter.Flush();
//                 Debug.Log($"Message '{message}' sent through the pipe.");
//             }
//             else
//             {
//                 // Handle the case when the pipe is not connected
//                 Debug.LogWarning("Pipe is not connected. Message not sent.");
//             }
//         }
//     }
//     catch (Exception ex)
//     {
//         Debug.LogError($"Error sending message through pipe: {ex.Message}");
//     }
// }
