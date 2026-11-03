using BankRaceCondition.Models;

namespace BankRaceCondition.Services;

public sealed class BankSimulator
{
    public async Task RunUnsafeAsync(
        BankAccount account,
        int workers,
        int operationsPerWorker,
        decimal amount)
    {
        Task[] tasks = Enumerable
            .Range(0, workers)
            .Select(_ => Task.Run(() =>
            {
                for (int i = 0; i < operationsPerWorker; i++)
                {
                    account.Deposit(amount);
                }
            }))
            .ToArray();

        // TODO 2: ожидаем выполенеия обхода задач
        await Task.WhenAll(tasks);
    }

    public async Task<int> RunWithdrawalsAsync(
        BankAccount account,
        int workers,
        int operationsPerWorker,
        decimal amount)
    {
        int successfulOperations = 0;

        Task[] tasks = Enumerable
            .Range(0, workers)
            .Select(_ => Task.Run(() =>
            {
                for (int i = 0; i < operationsPerWorker; i++)
                {
                    if (account.Withdraw(amount))
                    {
                        Interlocked.Increment(
                            ref successfulOperations);
                    }
                }
            }))
            .ToArray();

        await Task.WhenAll(tasks);

        return successfulOperations;
    }
}