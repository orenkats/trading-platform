public interface IUserFundsLogic
{
    Task<bool> DepositFundsAsync(Guid userId, decimal amount);
    Task<bool> WithdrawFundsAsync(Guid userId, decimal amount);
}
