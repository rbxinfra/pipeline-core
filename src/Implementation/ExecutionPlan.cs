namespace Roblox.Pipeline;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

/// <summary>
/// Default implementation of <see cref="IExecutionPlan{TInput, TOutput}"/>
/// </summary>
/// <typeparam name="TInput">The type of the input.</typeparam>
/// <typeparam name="TOutput">The type of the output.</typeparam>
public class ExecutionPlan<TInput, TOutput> : IExecutionPlan<TInput, TOutput>
{
    private readonly IList<IPipelineHandler<TInput, TOutput>> _Handlers = new List<IPipelineHandler<TInput, TOutput>>();

    /// <inheritdoc cref="IExecutionPlan{TInput, TOutput}.Handlers"/>
    public IReadOnlyCollection<IPipelineHandler<TInput, TOutput>> Handlers => _Handlers.ToArray();

    /// <inheritdoc cref="IExecutionPlan{TInput, TOutput}.RemoveHandler(int)"/>
    public void RemoveHandler(int index)
    {
        if (index >= _Handlers.Count || index < 0) throw new ArgumentException("index does not exist in handlers.", nameof(index));

        var handler = _Handlers[index];
        if (index > 0) _Handlers[index - 1].NextHandler = handler.NextHandler;

        _Handlers.Remove(handler);
    }

    /// <inheritdoc cref="IExecutionPlan{TInput, TOutput}.RemoveHandler{TPipelineHandler}()"/>
    public void RemoveHandler<TPipelineHandler>()
        where TPipelineHandler : IPipelineHandler<TInput, TOutput>
    {
        var index = GetHandlerIndex<TPipelineHandler>();
        if (index < 0) throw new ArgumentException(string.Format("No handler of type {0} was found.", typeof(TPipelineHandler)), nameof(TPipelineHandler));

        RemoveHandler(index);
    }

    /// <inheritdoc cref="IExecutionPlan{TInput, TOutput}.InsertHandler(int, IPipelineHandler{TInput, TOutput})"/>
    public void InsertHandler(int index, IPipelineHandler<TInput, TOutput> handler)
    {
        if (index < 0 || index > _Handlers.Count)
            throw new ArgumentException("index is not valid to add to in handlers. Index must be between 0, and the current handlers count.", nameof(index));
        if (handler == null) throw new ArgumentNullException(nameof(handler));
        if (_Handlers.Contains(handler))
            throw new ArgumentException("handler is already part of the execution plan. The same instance may only be used in one execution plan once at a time.", nameof(handler));

        if (index > 0) _Handlers[index - 1].NextHandler = handler;
        if (index < _Handlers.Count) handler.NextHandler = _Handlers[index];

        _Handlers.Insert(index, handler);
    }

    /// <inheritdoc cref="IExecutionPlan{TInput, TOutput}.AppendHandler(IPipelineHandler{TInput, TOutput})"/>
    public void AppendHandler(IPipelineHandler<TInput, TOutput> handler)
    {
        if (handler == null) throw new ArgumentNullException(nameof(handler));

        InsertHandler(_Handlers.Count, handler);
    }

    /// <inheritdoc cref="IExecutionPlan{TInput, TOutput}.PrependHandler(IPipelineHandler{TInput, TOutput})"/>
    public void PrependHandler(IPipelineHandler<TInput, TOutput> handler)
    {
        if (handler == null) throw new ArgumentNullException(nameof(handler));

        InsertHandler(0, handler);
    }

    /// <inheritdoc cref="IExecutionPlan{TInput, TOutput}.AddHandlerAfter{TPipelineHandler}(IPipelineHandler{TInput, TOutput})"/>
    public void AddHandlerAfter<T>(IPipelineHandler<TInput, TOutput> handler)
        where T : IPipelineHandler<TInput, TOutput>
    {
        if (handler == null) throw new ArgumentNullException(nameof(handler));

        int index = GetHandlerIndex<T>();
        if (index < 0) throw new ArgumentException(string.Format("No handler of type {0} was found.", typeof(T)), nameof(T));

        InsertHandler(index + 1, handler);
    }

    /// <inheritdoc cref="IExecutionPlan{TInput, TOutput}.AddHandlerBefore{TPipelineHandler}(IPipelineHandler{TInput, TOutput})"/>
    public void AddHandlerBefore<T>(IPipelineHandler<TInput, TOutput> handler)
        where T : IPipelineHandler<TInput, TOutput>
    {
        if (handler == null) throw new ArgumentNullException(nameof(handler));

        int index = GetHandlerIndex<T>();
        if (index < 0) throw new ArgumentException(string.Format("No handler of type {0} was found.", typeof(T)), nameof(T));

        InsertHandler(index, handler);
    }

    /// <inheritdoc cref="IExecutionPlan{TInput, TOutput}.ClearHandlers"/>
    public void ClearHandlers() => _Handlers.Clear();

    /// <inheritdoc cref="IExecutionPlan{TInput, TOutput}.Execute(TInput)"/>
    public TOutput Execute(TInput input)
    {
        if (!_Handlers.Any()) throw new NoHandlersException();

        var context = new ExecutionContext<TInput, TOutput>(input);
        _Handlers.First().Invoke(context);

        return context.Output;
    }

    /// <inheritdoc cref="IExecutionPlan{TInput, TOutput}.ExecuteAsync(TInput, CancellationToken)"/>
    public async Task<TOutput> ExecuteAsync(TInput input, CancellationToken cancellationToken)
    {
        if (!_Handlers.Any()) throw new NoHandlersException();

        var context = new ExecutionContext<TInput, TOutput>(input);
        await _Handlers.First().InvokeAsync(context, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);

        return context.Output;
    }

    private int GetHandlerIndex<T>()
    {
        for (int i = 0; i < _Handlers.Count; i++)
            if (_Handlers[i] is T)
                return i;

        return -1;
    }
}
