using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SignalMatrixCalculator
{
    public class MatrixHandler
    {
        private static StringBuilder m_SBBuff = new StringBuilder();

        private const char m_Separator = ',';

        private static int m_RowCount = 3;
        private static int m_ColumnCount = 3;
        private static int m_SingleCount = 3;

        // 广度优先搜索算法，寻找使所有按钮上的数字相同的最短路径
        public static async Task<List<(int, int)>> Bfs(int[,] _initialState, List<(int, int)> _ignorePoint, int _rowCount, int _columnCount, int _singleCount, Action<int> _showVisitedNodeCountAction, CancellationToken _cancellationToken)
        //public static List<(int, int)> Bfs(int[,] _initialState, List<(int, int)> _ignorePoint, int _rowCount, int _columnCount, int _singleCount,Action<int> _showVisitedNodeCountAction)
        {
            m_RowCount = _rowCount;
            m_ColumnCount = _columnCount;
            m_SingleCount = _singleCount;

            var queue = new Queue<Node>(); // 创建队列用于BFS
            var visited = new HashSet<string>(); // 创建集合用于存储已访问的状态
            queue.Enqueue(new Node(_initialState)); // 将初始状态加入队列
            visited.Add(StateToString(_initialState)); // 将初始状态加入已访问集合

            List<(int, int)> result = null;

            while (queue.Count > 0) // 当队列不为空时继续循环
            {
                Node currentNode = queue.Dequeue(); // 取出队首节点

                if (currentNode.IsGoal(_ignorePoint)) // 如果当前节点是目标状态
                {
                    var path = new Stack<(int, int)>(); // 创建栈用于存储路径
                    Node node = currentNode; // 从当前节点开始回溯

                    while (node.Parent != null) // 回溯直到根节点
                    {
                        path.Push(node.Action); // 将动作压入栈中
                        node = node.Parent; // 移动到父节点
                    }

                    result = new List<(int, int)>(path);
                    break;
                }

                foreach (Node neighbor in currentNode.GetNeighbors(_ignorePoint)) // 遍历当前节点的所有邻居节点
                {
                    string neighborState = StateToString(neighbor.State); // 将邻居状态转换为字符串
                    if (!visited.Contains(neighborState)) // 如果邻居状态未被访问过
                    {
                        visited.Add(neighborState); // 将邻居状态加入已访问集合
                        queue.Enqueue(neighbor); // 将邻居节点加入队列
                    }


                }

                _showVisitedNodeCountAction(visited.Count);

                await Task.Delay(1);
                //Thread.Sleep(1);

                if (_cancellationToken.IsCancellationRequested)
                    break;
            }

            m_SBBuff.Clear();
            Node.m_NeighborBuffList.Clear();

            return result; // 没有找到解决方案
        }

        // 将二维数组状态转换为字符串表示
        private static string StateToString(int[,] state)
        {
            m_SBBuff.Clear();

            for (int x = 0; x < m_RowCount; x++)
            {
                for (int y = 0; y < m_ColumnCount; y++)
                {
                    m_SBBuff.Append(state[x, y]); // 追加当前单元格的值

                    if (y < m_ColumnCount - 1 || x < m_RowCount - 1) // 如果不是最后一个元素
                        m_SBBuff.Append(m_Separator); // 追加逗号分隔符
                }
            }

            return m_SBBuff.ToString();
        }

        private class Node
        {
            public static List<Node> m_NeighborBuffList = new List<Node>();

            // 表示当前节点的状态（x*y网格）
            public int[,] State { get; private set; }
            // 表示父节点
            public Node Parent { get; private set; }
            // 表示采取的动作（按下的按钮坐标）
            public (int, int) Action { get; private set; }

            // 构造函数，初始化节点状态、父节点和动作
            public Node(int[,] state, Node parent = null, (int, int) action = default)
            {
                State = (int[,])state.Clone(); // 克隆状态以避免引用传递
                Parent = parent; // 设置父节点
                Action = action; // 设置动作
            }

            // 获取当前节点的所有邻居节点
            public List<Node> GetNeighbors(List<(int, int)> _ignorePoint)
            {
                m_NeighborBuffList.Clear();

                for (int row = 0; row < m_RowCount; row++) // 遍历每一行
                {
                    for (int column = 0; column < m_ColumnCount; column++) // 遍历每一列
                    {
                        if (_ignorePoint.Contains((row, column)))
                            continue;

                        int[,] newState = (int[,])State.Clone(); // 克隆当前状态
                        newState[row, column] = (newState[row, column] + 1) % m_SingleCount; // 更新按下按钮的值

                        // 更新相邻单元格的值
                        if (row > 0)
                            newState[row - 1, column] = (newState[row - 1, column] + 1) % m_SingleCount; // 上方单元格
                        if (row < m_RowCount - 1)
                            newState[row + 1, column] = (newState[row + 1, column] + 1) % m_SingleCount; // 下方单元格
                        if (column > 0)
                            newState[row, column - 1] = (newState[row, column - 1] + 1) % m_SingleCount; // 左侧单元格
                        if (column < m_ColumnCount - 1)
                            newState[row, column + 1] = (newState[row, column + 1] + 1) % m_SingleCount; // 右侧单元格

                        m_NeighborBuffList.Add(new Node(newState, this, (row, column))); // 添加新节点到邻居列表
                    }
                }
                return m_NeighborBuffList; // 返回所有邻居节点
            }

            // 检查当前状态是否为目标状态（所有按钮上的数字相同）
            public bool IsGoal(List<(int, int)> _ignorePoint)
            {
                int firstValue = State[0, 0]; // 获取第一个按钮的值

                for (int row = 0; row < m_RowCount; row++) // 遍历每一行
                {
                    for (int column = 0; column < m_ColumnCount; column++) // 遍历每一列
                    {
                        if (!_ignorePoint.Contains((row, column)) && State[row, column] != firstValue) // 如果有任何按钮的值不同
                            return false; // 不是目标状态
                    }
                }

                return true; // 所有按钮的值相同，是目标状态
            }
        }
    }
}