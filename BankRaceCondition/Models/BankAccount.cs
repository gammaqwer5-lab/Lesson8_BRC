namespace BankRaceCondition.Models;

public sealed class BankAccount
{
    private decimal _balance;
    private readonly object _sync = new();
    public decimal Balance
    {
        get
        {
            lock (_sync)
            {
                return _balance;
            }
        }
    }

    public void DepositUnsafe(decimal amount)
    {
        // TODO 1:
        // Проверьте, что сумма пополнения больше нуля.
        // Если нет — выбросьте ArgumentOutOfRangeException.


        // TODO 2:
        // Считайте текущее значение баланса в локальную переменную.

        //Deposit(amount);

        // TODO 3:
        // Искусственно увеличьте вероятность состояния гонки:
        // используйте Thread.SpinWait(1000).

        //Thread.SpinWait(1000);

        // TODO 4:
        // Прибавьте сумму к локальной переменной
        // и запишите результат обратно в _balance.

        //Deposit(amount);
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount));
        }

        lock (_sync)
        {
            _balance += amount;
        }
    }

    public bool Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount));
        }
        lock (_sync)
        {
            if (_balance < amount)
            {
                return false;
            }
            _balance -= amount;
            return true;
        }
    }
}