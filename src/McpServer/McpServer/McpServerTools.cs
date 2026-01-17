using ModelContextProtocol.Server;
using System.ComponentModel;

namespace McpServer;

[McpServerToolType]
public sealed class McpServerTools
{
    private readonly ILogger<McpServerTools> _logger;

    public McpServerTools(ILogger<McpServerTools> logger)
    {
        _logger = logger;
    }

    [McpServerTool, Description(
        "Health check tool. Behaves as follows:\n" +
        "- If message is empty, respond with server health\n" +
        "- Otherwise echo the message"
    )]
    public async Task<string> Ping([Description("Ping message.")] string message)
    {
        await Task.CompletedTask;

        if (string.IsNullOrWhiteSpace(message))
            _logger.LogInformation("Ping received with no message.");
        else
            _logger.LogInformation("Ping received with message: {Message}", message);

        return string.IsNullOrWhiteSpace(message)
            ? "✅ MCP server is alive."
            : $"✅ MCP server received: {message}";
    }
}