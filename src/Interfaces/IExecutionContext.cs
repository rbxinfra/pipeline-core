namespace Roblox.Pipeline;

/// <summary>
/// Represents the context for a pipeline execution.
/// </summary>
/// <typeparam name="TInput">The type of the input.</typeparam>
/// <typeparam name="TOutput">The type of the output.</typeparam>
public interface IExecutionContext<TInput, TOutput>
{
    /// <summary>
    /// Gets or sets the input.
    /// </summary>
    TInput Input { get; set; }

    /// <summary>
    /// Gets or sets the output.
    /// </summary>
    TOutput Output { get; set; }
}
