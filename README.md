# Remote MCP Server using .NET SDK with VS Code Support

This project is a C# implementation of a Model Context Protocol (MCP) server that exposes weather forecast and health check tools. This server integrates with GitHub Copilot in VS Code, allowing AI assistants to call your custom tools.

## Overview

This project demonstrates how to build an HTTP-based MCP server using the .NET SDK. The server exposes two tools:

- **ping**: Health check with optional message echo
- **get_weather**: Weather forecast retrieval for cities

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download) or later
- [Visual Studio Code](https://code.visualstudio.com/)
- [GitHub Copilot extension](https://marketplace.visualstudio.com/items?itemName=GitHub.copilot) (with MCP support)
- [C# Dev Kit extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit) (recommended)

## Project Structure

```text
remote-MCP-servers-using-dotnet-sdk-vs-code-support/
├── src/
│   └── McpServer/
│       └── McpServer/
│           ├── McpServerTools.cs      # Tool definitions
│           ├── Program.cs             # Server entry point
│           └── McpServer.csproj       # Project file
├── .vscode/
│   └── mcp.json                       # MCP server configuration
└── README.md
```

## Getting Started

### 1. Build and Run the Server

Open a terminal in the project root and run:

```powershell
dotnet run --project .\src\McpServer\McpServer\McpServer.csproj
```

You should see output indicating the server is running:

```powershell
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://0.0.0.0:8081
```

### 2. Configure VS Code

Create or verify `.vscode/mcp.json` in your workspace:

```json
{
  "servers": {
    "local-mcp-server": {
      "url": "http://0.0.0.0:8081/mcp",
      "type": "http"
    }
  }
}
```

### 3. Connect to the Server in VS Code

1. **Open Command Palette**: Press `Ctrl + Shift + P` (Windows/Linux) or `Cmd + Shift + P` (Mac)

2. **List MCP Servers**: Type and select `List MCP Servers`

3. **Select your server**: Choose `local-mcp-server` from the list

4. **Available actions**:
   - **Start**: Starts the MCP server connection
   - **Stop**: Stops the MCP server connection
   - **Restart**: Restarts the MCP server connection
   - **Show Configuration**: Displays the server configuration from `mcp.json`
   - **Show Output**: Opens the output panel showing server logs and tool discovery
   - **Configure Model Access**: Configures which AI models can access this MCP server

### 4. Verify Server Status

After starting the server, check the output panel for:

```text
[info] Connection state: Running
[info] Discovered 2 tools
```

✅ If you see this, the server is ready to use!

⚠️ If you see warnings about tool descriptions, ensure all methods in [McpServerTools.cs](src/McpServer/McpServer/McpServerTools.cs) have `[Description]` attributes.

## Testing the MCP Server

### Ping Test

Once the server is running, test it using the `ping` tool in GitHub Copilot Chat:

Open chat windows and start conversation

#### Test 1: Basic Health Check

```text
Ask: "Ping the server"
Expected Response: ✅ MCP server is alive.
```
<img width="397" height="503" alt="image" src="https://github.com/user-attachments/assets/79236db9-2cc9-4e69-a332-54ce55c3e372" />


<img width="402" height="297" alt="image" src="https://github.com/user-attachments/assets/639c8fae-235f-4d2e-99f8-91a44247d883" />

#### Test 2: Echo Message

```text
Ask: "Ping the server with message 'Hello MCP'"
Expected Response: ✅ MCP server received: Hello MCP
```

### Weather Forecast Test

Test the weather forecast functionality:

```text
Ask: "What's the weather in London?"
Expected Response: Weather forecast data for London
```
<img width="378" height="513" alt="image" src="https://github.com/user-attachments/assets/bc53b7fb-bd69-4f36-9900-c844296200be" />


<img width="402" height="435" alt="image" src="https://github.com/user-attachments/assets/18d71745-1583-4670-9177-69fdaefbb907" />

## Available Tools

The `local-mcp-server` provides the following tools:

### ping

**Description**: Health check tool that verifies the MCP server is running. If a message is provided, it echoes it back; otherwise, returns server health status.

**Parameters**:

- `message` (string, optional): Optional message to echo back. If empty, returns health status.

**Example Usage**:

- "Ping the server"
- "Run a health check on the MCP server"

### get_weather

**Description**: Retrieves the current weather forecast for a specified city.

**Parameters**:

- `city` (string, required): The name of the city to get weather forecast for.

**Example Usage**:

- "What's the weather in Paris?"
- "Get weather forecast for Tokyo"

## Troubleshooting

### Server Not Discovering Tools

**Symptom**:

```powershell
[warning] Tool get_weather does not have a description
[warning] Tool ping does not have a description
```

**Solution**: Ensure all tool methods have `[Description]` attributes at the method level in [McpServerTools.cs](src/McpServer/McpServer/McpServerTools.cs):

```csharp
[McpServerTool]
[Description("Health check tool that verifies the MCP server is running...")]
public async Task<string> Ping([Description("Optional message...")] string message)
```

### Server Won't Start

**Symptom**: Port 8081 already in use

**Solution**:

1. Stop any existing instances of the server
2. Check for processes using port 8081: `netstat -ano | findstr :8081`
3. Kill the process or change the port in `Program.cs`

### Tools Not Visible in Copilot Chat

**Symptom**: Server is running but tools don't appear in chat

**Solution**:

1. Verify the server is running: Check output panel
2. Restart VS Code: `Ctrl + Shift + P` → "Developer: Reload Window"
3. Reconnect to the server: Use "List MCP Servers" command
4. Ensure GitHub Copilot extension is up to date

### Connection State: Stopped

**Symptom**: Server keeps stopping automatically

**Solution**:

1. Check server logs for errors: "Show Output" command
2. Verify the server is running on the correct port (8081)
3. Test the endpoint manually: `curl -k -v http://localhost:8081/api/healthz`

## Resources

- [Build a Model Context Protocol (MCP) Server in C#](https://devblogs.microsoft.com/dotnet/build-a-model-context-protocol-mcp-server-in-csharp/) - Official Microsoft tutorial
- [C# SDK Repository](https://github.com/modelcontextprotocol/csharp-sdk) - Official MCP C# SDK
- [MCP Specification](https://modelcontextprotocol.io/specification/2025-11-25) - Protocol documentation

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

MIT License

## Repository

[Repository](https://github.com/azurecorner/remote-MCP-servers-using-dotnet-sdk-vs-code-support)
