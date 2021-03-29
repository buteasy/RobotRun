using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;

namespace RobotRun.Presistence
{
    public interface IRobotRunDataAccess
    {
        public event EventHandler<int> GameTableSizeChanged;
        Task<GameTable> LoadAsync(String path);
        Task SaveAsync(String path, GameTable table);
    }
}
