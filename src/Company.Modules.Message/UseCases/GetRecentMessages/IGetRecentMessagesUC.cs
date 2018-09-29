using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Company.Modules.Message.Database;

namespace Company.Modules.Message.UseCases
{
    public interface IGetRecentMessagesUC : IUseCase<ReadOnlyObservableCollection<Database.Message>>
    {
    }
}
