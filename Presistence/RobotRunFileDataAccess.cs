using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RobotRun.Presistence
{
    public class RobotRunFileDataAccess : IRobotRunDataAccess
    {
        public event EventHandler<int> GameTableSizeChanged;
        public async Task<GameTable> LoadAsync(String path)
        {
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    String line = await reader.ReadLineAsync();
                    Int32 tableSize = Int32.Parse(line);
                    GameTable table = new GameTable(tableSize);
                    GameTableSizeChanged(this, tableSize);
                    line = await reader.ReadLineAsync();
                    table.SetGameTime(Int32.Parse(line));

                    String[] numbers;
                    for(Int32 i = 0; i < tableSize; i++)
                    {
                        line = await reader.ReadLineAsync();
                        numbers = line.Split(' ');
                        for (Int32 j = 0; j < tableSize; j++)
                        {
                            table.SetValue(i, j, Int32.Parse(numbers[j]));
                        }
                    }
                    return table;
                }
            }
            catch
            {
                throw new RobotRunDataException();
            }
            
        }

        public async Task SaveAsync(String path, GameTable table)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.Write(table.TableSize);
                    await writer.WriteLineAsync();
                    writer.Write(table.GetGameTime());
                    await writer.WriteLineAsync();
                    for (Int32 i = 0; i < table.TableSize; ++i)
                    {
                        for(Int32 j = 0; j < table.TableSize; ++j)
                        {
                            await writer.WriteAsync(table.GetValue(i, j) + " ");
                        }
                        await writer.WriteLineAsync();
                    }
                }
            }
            catch
            {
                throw new RobotRunDataException();
            }
        }
    }
}
