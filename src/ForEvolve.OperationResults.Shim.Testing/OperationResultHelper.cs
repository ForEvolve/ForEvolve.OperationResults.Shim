﻿using ForEvolve.AspNetCore;
using ForEvolve.Contracts.Errors;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace ForEvolve.AspNetCore
{
    public class OperationResultHelper
    {
        public IOperationResult CreateSuccess(Action<Mock<IOperationResult>> setup = null)
        {
            return CreateResult(expectSuccess: true);
        }

        public IOperationResult CreateFailure(IEnumerable<Error> errors = null, IEnumerable<Exception> exceptions = null, Action<Mock<IOperationResult>> setup = null)
        {
            return CreateResult(
                expectSuccess: false,
                errors: errors ?? (exceptions == null ? new Error[] { new Error("CreateFailure", "Error message generated by OperationResultHelper.CreateFailure") } : null),
                exceptions: exceptions
            );
        }

        private IOperationResult CreateResult(bool expectSuccess = true, IEnumerable<Error> errors = null, IEnumerable<Exception> exceptions = null, Action<Mock<IOperationResult>> setup = null)
        {
            var resultMock = new Mock<IOperationResult>();
            DefaultResultSetup(resultMock, errors, exceptions);
            resultMock.Setup(x => x.Succeeded).Returns(expectSuccess);
            setup?.Invoke(resultMock);
            return resultMock.Object;
        }

        private static void SetupValueProperty<TResult>(Mock<IOperationResult<TResult>> resultMock)
        {
            resultMock.SetupProperty(x => x.Value);
            resultMock.Setup(x => x.HasResult()).Returns(() => resultMock.Object.Value != null);
        }

        private static void DefaultResultSetup(Mock<IOperationResult> resultMock, IEnumerable<Error> errors = null, IEnumerable<Exception> exceptions = null)
        {
            resultMock.Setup(x => x.Errors).Returns(errors ?? new Error[0]);
            resultMock.Setup(x => x.HasError())
                .Returns(() => resultMock.Object.Errors?.Count() > 0);

            resultMock.Setup(x => x.Exceptions).Returns(exceptions ?? new Exception[0]);
            resultMock.Setup(x => x.HasException())
                .Returns(() => resultMock.Object.Exceptions?.Count() > 0);

            resultMock.Setup(x => x.AddError(It.IsAny<Error>())).Verifiable();
            resultMock.Setup(x => x.AddErrors(It.IsAny<IEnumerable<Error>>())).Verifiable();
            resultMock.Setup(x => x.AddErrorsFrom(It.IsAny<IdentityResult>())).Verifiable();
            resultMock.Setup(x => x.AddErrorsFrom(It.IsAny<IOperationResult>())).Verifiable();
            resultMock.Setup(x => x.AddException(It.IsAny<Exception>())).Verifiable();
        }
    }
}