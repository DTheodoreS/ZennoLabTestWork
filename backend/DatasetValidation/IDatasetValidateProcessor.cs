namespace backend.DatasetValidation
{
    using System.Collections.Generic;

    public interface IDatasetValidateProcessor
    {
        bool IsValid { get; }

        List<DatasetValidateResult> GetErrors();
    }
}
