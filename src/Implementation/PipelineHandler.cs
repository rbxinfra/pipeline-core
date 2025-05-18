namespace Roblox.Pipeline;

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Default implementation of <see cref="IPipelineHandler{TInput, TOutput}"/>
/// </summary>
/// <typeparam name="TInput">The type of the input.</typeparam>
/// <typeparam name="TOutput">The type of the output.</typeparam>
public abstract class PipelineHandler<TInput, TOutput> : IPipelineHandler<TInput, TOutput>
{
    /// <inheritdoc cref="IPipelineHandler{TInput, TOutput}.NextHandler"/>
    [ExcludeFromCodeCoverage]
    public virtual IPipelineHandler<TInput, TOutput> NextHandler { get; set; }

    /// <inheritdoc cref="IPipelineHandler{TInput, TOutput}.Invoke(IExecutionContext{TInput, TOutput})"/>
    public virtual void Invoke(IExecutionContext<TInput, TOutput> context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));
        if (NextHandler == null) return;

        NextHandler.Invoke(context);
    }

    /// <inheritdoc cref="IPipelineHandler{TInput, TOutput}.InvokeAsync(IExecutionContext{TInput, TOutput}, CancellationToken)"/>
    public virtual async Task InvokeAsync(IExecutionContext<TInput, TOutput> context, CancellationToken cancellationToken)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));
        if (NextHandler == null) return;

        await NextHandler.InvokeAsync(context, cancellationToken);
    }
}
