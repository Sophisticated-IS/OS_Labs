using System;

namespace HashTable
{
    /// <summary>
    /// Элемент данных хеш таблицы.
    /// </summary>
    public sealed class Item
    {
        /// <summary>
        /// Ключ.
        /// </summary>
        public int Key { get; private set; }

        /// <summary>
        /// Хранимые данные.
        /// </summary>
        public int Value { get; private set; }

        /// <summary>
        /// Создать новый экземпляр хранимых данных Item.
        /// </summary>
        /// <param name="key"> Ключ. </param>
        /// <param name="value"> Значение. </param>
        public Item(int key, int value)
        {
            // Устанавливаем значения.
            Key = key;
            Value = value;
        }
    }
}