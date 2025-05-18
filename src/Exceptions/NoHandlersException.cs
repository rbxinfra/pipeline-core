namespace Roblox.Pipeline;

using System;

/// <summary>
/// Exception thrown when there are no handlers in a <see cref="IExecutionPlan{TInput, TOutput}"/>
/// </summary>
public class NoHandlersException : Exception
{
}
