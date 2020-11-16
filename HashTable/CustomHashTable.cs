using System;
using System.Collections.Generic;
using System.Linq;

namespace HashTable
{
    public class CustomHashTable
    {
        //Количество строк таблицы и основание хеш-функции (число k) 
        private readonly int _k;

        /// <summary>
        /// Коллекция хранимых данных.
        /// </summary>
        /// <remarks>
        /// Представляет собой словарь, ключ которого представляет собой хеш ключа хранимых данных,
        /// а значение это список элементов с одинаковым хешем ключа.
        /// </remarks>
        private Dictionary<int, List<Item>> _items;


        public CustomHashTable(int k)
        {
            _k = k;
            _items = new Dictionary<int, List<Item>>();
        }


        public void Insert(int key, int value)
        {
            // Создаем новый экземпляр данных.
            var item = new Item(key, value);

            // Получаем хеш ключа
            var hash = GetHash(item.Key);

            List<Item> hashTableItem;
            if (_items.ContainsKey(hash))
            {
                // Получаем элемент хеш таблицы.
                hashTableItem = _items[hash];

                // Проверяем наличие внутри коллекции значения с полученным ключом.
                // Если такой элемент найден, то сообщаем об ошибке.
                var oldElementWithKey = hashTableItem.SingleOrDefault(i => i.Key == item.Key);
                if (oldElementWithKey != null)
                {
                    throw new ArgumentException(
                        $"Хеш-таблица уже содержит элемент с ключом {key}. Ключ должен быть уникален.", nameof(key));
                }

                // Добавляем элемент данных в коллекцию элементов хеш таблицы.
                _items[hash].Add(item);
            }
            else
            {
                //Количество строк таблицы 
                if (_items.Count >= _k) return;

                // Создаем новую коллекцию.
                hashTableItem = new List<Item> {item};

                // Добавляем данные в таблицу.
                _items.Add(hash, hashTableItem);
            }
        }

        public int GetHash(int x)
        {
            return x % _k;
        }
    }
}