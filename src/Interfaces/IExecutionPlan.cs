namespace Roblox.Pipeline;

using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

/// <summary>
/// Represents a system for executing a list of <see cref="IPipelineHandler{TInput, TOutput}"/>
/// </summary>
/// <typeparam name="TInput">The type of the input.</typeparam>
/// <typeparam name="TOutput">The type of the output.</typeparam>
public interface IExecutionPlan<TInput, TOutput>
{
    /// <summary>
    /// Gets the <see cref="IPipelineHandler{TInput, TOutput}"/>s
    /// </summary>
    IReadOnlyCollection<IPipelineHandler<TInput, TOutput>> Handlers { get; }

    /// <summary>
    /// Remove a <see cref="IPipelineHandler{TInput, TOutput}"/> at the specified index.
    /// </summary>
    /// <param name="index">The index of the pipeline handler.</param>
    void RemoveHandler(int index);

    /// <summary>
    /// Removes a <see cref="IPipelineHandler{TInput, TOutput}"/> of type <typeparamref name="TPipelineHandler"/>
    /// </summary>
    /// <typeparam name="TPipelineHandler">The type of the <see cref="IPipelineHandler{TInput, TOutput}"/></typeparam>
    void RemoveHandler<TPipelineHandler>()
        where TPipelineHandler : IPipelineHandler<TInput, TOutput>;

    /// <summary>
    /// Appends the specified <see cref="IPipelineHandler{TInput, TOutput}"/> to the plan.
    /// </summary>
    /// <param name="handler">The <see cref="IPipelineHandler{TInput, TOutput}"/></param>
    void AppendHandler(IPipelineHandler<TInput, TOutput> handler);

    /// <summary>
    /// Prepends the specified <see cref="IPipelineHandler{TInput, TOutput}"/> to the plan.
    /// </summary>
    /// <param name="handler">The <see cref="IPipelineHandler{TInput, TOutput}"/></param>
    void PrependHandler(IPipelineHandler<TInput, TOutput> handler);

    /// <summary>
    /// Adds a <see cref="IPipelineHandler{TInput, TOutput}"/> after the <see cref="IPipelineHandler{TInput, TOutput}"/> in <typeparamref name="TPipelineHandler"/>
    /// </summary>
    /// <typeparam name="TPipelineHandler">The type of the <see cref="IPipelineHandler{TInput, TOutput}"/></typeparam>
    /// <param name="handler">The <see cref="IPipelineHandler{TInput, TOutput}"/> to add.</param>
    void AddHandlerAfter<TPipelineHandler>(IPipelineHandler<TInput, TOutput> handler)
        where TPipelineHandler : IPipelineHandler<TInput, TOutput>;

    /// <summary>
    /// Adds a <see cref="IPipelineHandler{TInput, TOutput}"/> before the <see cref="IPipelineHandler{TInput, TOutput}"/> in <typeparamref name="TPipelineHandler"/>
    /// </summary>
    /// <typeparam name="TPipelineHandler">The type of the <see cref="IPipelineHandler{TInput, TOutput}"/></typeparam>
    /// <param name="handler">The <see cref="IPipelineHandler{TInput, TOutput}"/> to add.</param>
    void AddHandlerBefore<TPipelineHandler>(IPipelineHandler<TInput, TOutput> handler)
        where TPipelineHandler : IPipelineHandler<TInput, TOutput>;

    /// <summary>
    /// Inserts a <see cref="IPipelineHandler{TInput, TOutput}"/> at the specified instance.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <param name="handler">The <see cref="IPipelineHandler{TInput, TOutput}"/></param>
    void InsertHandler(int index, IPipelineHandler<TInput, TOutput> handler);

    /// <summary>
    /// Clear the handlers list.
    /// </summary>
    void ClearHandlers();

    /// <summary>
    /// Execute the <see cref="IExecutionPlan{TInput, TOutput}"/>
    /// </summary>
    /// <param name="input">The <typeparamref name="TInput"/></param>
    /// <returns>The <typeparamref name="TOutput"/></returns>
    TOutput Execute(TInput input);

    /// <summary>
    /// Execute the <see cref="IExecutionPlan{TInput, TOutput}"/> asynchronously
    /// </summary>
    /// <param name="input">The <typeparamref name="TInput"/></param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/></param>
    /// <returns>The <typeparamref name="TOutput"/></returns>
    Task<TOutput> ExecuteAsync(TInput input, CancellationToken cancellationToken);
}
