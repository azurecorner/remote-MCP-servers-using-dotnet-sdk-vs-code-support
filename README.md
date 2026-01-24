# remote-MCP-servers-using-dotnet

## Resources

- [Build a Model Context Protocol (MCP) Server in C#](https://devblogs.microsoft.com/dotnet/build-a-model-context-protocol-mcp-server-in-csharp/)
- [Video Tutorial](https://youtu.be/iS25RFups4A?si=prIH_U56t4-HYrai)
- [C# SDK Repository](https://github.com/modelcontextprotocol/csharp-sdk)

## Use copilot

```powershell

dotnet run --project .\src\McpServer\McpServer\McpServer.csproj

```

```json
{
  "servers": {
    "local-mcp-server": {
      "url": "http://0.0.0.0:8081/mcp",
      "type": "http"
    }
  },
  "inputs": [
 
  ]
}

```

## Managing the MCP Server in VS Code

Once you have the MCP server configuration in `.vscode/mcp.json`, you can manage it through the VS Code Command Palette:

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

### Verifying Server Status

After starting the server, check the output panel for:

```text
[info] Connection state: Running
[info] Discovered 2 tools
```

If you see warnings about tool descriptions, ensure all methods in `McpServerTools.cs` have `[Description]` attributes.

## Testing the MCP Server

### Ping Test

Once the server is running, you can test it using the `ping` tool:

#### Test 1: Basic Health Check

```text

Ask: "Ping the server"
Expected Response: ✅ MCP server is alive.
```

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

### Available Tools

The `local-mcp-server` provides:

- **ping**: Health check with optional message echo
  - Parameter: `message` (optional string)
- **get_weather**: Retrieves weather forecast for a city
  - Parameter: `city` (required string)



