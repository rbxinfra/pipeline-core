namespace Roblox.Pipeline;

using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Represents a hander to execute work in a pipeline.
/// </summary>
/// <typeparam name="TInput">The type of the input.</typeparam>
/// <typeparam name="TOutput">The type of the output.</typeparam>
public interface IPipelineHandler<TInput, TOutput>
{
    /// <summary>
    /// Gets or sets the next handler.
    /// </summary>
    /// <remarks>If this is null, then you can consider this to be the last handler in the list.</remarks>
    IPipelineHandler<TInput, TOutput> NextHandler { get; set; }

    /// <summary>
    /// Invokes this handler.
    /// </summary>
    /// <param name="context">The <see cref="IExecutionContext{TInput, TOutput}"/></param>
    void Invoke(IExecutionContext<TInput, TOutput> context);

    /// <summary>
    /// Invokes this handler asynchronously
    /// </summary>
    /// <param name="context">The <see cref="IExecutionContext{TInput, TOutput}"/></param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/></param>
    Task InvokeAsync(IExecutionContext<TInput, TOutput> context, CancellationToken cancellationToken);
}
