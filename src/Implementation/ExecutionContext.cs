namespace Roblox.Pipeline;

/// <summary>
/// Default implementation for <see cref="IExecutionContext{TInput, TOutput}"/>
/// </summary>
/// <typeparam name="TInput">The type of the input.</typeparam>
/// <typeparam name="TOutput">The type of the output.</typeparam>
internal class ExecutionContext<TInput, TOutput> : IExecutionContext<TInput, TOutput>
{
    /// <inheritdoc cref="IExecutionContext{TInput, TOutput}.Input"/>
    public TInput Input { get; set; }

    /// <inheritdoc cref="IExecutionContext{TInput, TOutput}.Output"/>
    public TOutput Output { get; set; }

    /// <summary>
    /// Constructs a new instance of <see cref="ExecutionContext{TInput, TOutput}"/>
    /// </summary>
    /// <param name="input">The <typeparamref name="TInput"/></param>
    public ExecutionContext(TInput input)
    {
        Input = input;
    }
}
