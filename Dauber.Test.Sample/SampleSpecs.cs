using System;
using FakeItEasy;
using Shouldly;

namespace Dauber.Test.Sample
{
    public interface ICalculator
    {
        double Add(double a, double b);
    }

    public class Calculator : ICalculator
    {
        public double Add(double a, double b)
        {
            return a + b;
        }
    }

    public interface IAccountProcessor
    {
        double CurrentBalance { get; }
        void AddMoney(double money);
        void AddInterest(double interest);
    }

    public class AccountProcessor : IAccountProcessor
    {
        private readonly ICalculator _calculator;

        public AccountProcessor(ICalculator calculator)
        {
            _calculator = calculator;
        }

        public double CurrentBalance { get; set; }
        public void AddMoney(double money)
        {
            CurrentBalance = _calculator.Add(CurrentBalance, money);
        }

        public void AddInterest(double interest)
        {
            CurrentBalance += CurrentBalance*interest;            
        }
    }

    public class AccountProcessorSpecs
    {
        public class when_adding_money
        {
            private readonly IAccountProcessor _accountProcessor;

            public when_adding_money(
                ICalculator fakeCalculator, 
                IAccountProcessor accountProcessor)
            {
                A.CallTo(() => fakeCalculator.Add(A<double>._, A<double>._)).Returns(30);
                _accountProcessor = accountProcessor;                
                _accountProcessor.AddMoney(A<double>._);
            }

            public void it_should_add_money_to_the_current_balance()
            {
                _accountProcessor.CurrentBalance.ShouldBe(30);
            }
        }        
    }
}
