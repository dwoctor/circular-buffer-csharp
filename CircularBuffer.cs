using System;

/// <summary>
/// A Circular Buffer
/// </summary>
public class CircularBuffer <T>
{
    #region Fields
    /// <summary>
    /// Stores the Data
    /// </summary>
    private T[] _array;

    /// <summary>
    /// Stores the Index of where the array Starts
    /// </summary>
    private Int32 _start;

    /// <summary>
    /// Stores the Index of where the array Ends
    /// </summary>
    private Int32 _end;

    /// <summary>
    /// Stores the Number of Elements in the array
    /// </summary>
    private Int32 _size;

    /// <summary>
    /// Indicates whether the array is Dynamic
    /// </summary>
    private Boolean _isDynamic;

    /// <summary>
    /// Indicates whether the array is Infinite
    /// </summary>
    private Boolean _isInfinite;
    #endregion

    #region Constructors
    /// <summary>
    /// Constructs the Array
    /// </summary>
    public CircularBuffer() : this(0, false, false) { }

    /// <summary>
    /// Constructs the Array
    /// </summary>
    /// <param name="size">The Size of the Array</param>
    public CircularBuffer(Int32 size) : this(size, false, false) { }

    /// <summary>
    /// Constructs the Array
    /// </summary>
    /// <param name="isSorted">Is the Array Sorted</param>
    public CircularBuffer() : this(0, false, false) { }

    /// <summary>
    /// Constructs the Array
    /// </summary>
    /// <param name="size">The Size of the Array</param>
    /// <param name="isSorted">Is the Array Sorted</param>
    public CircularBuffer(Int32 size) : this(size, false, false) { }

    /// <summary>
    /// Constructs the Array
    /// </summary>
    /// <param name="size">The Size of the Array</param>
    /// <param name="isInfinite">An Infinite CircularBuffer will override old Elements in the Array when it is Full</param>
    public CircularBuffer(Int32 size, Boolean isInfinite, Boolean isDynamic)
    {
        _array = new T[size];
        _start = 0;
        _end = 0;
        _size = 0;
        _isDynamic = isDynamic;
        _isInfinite = isInfinite;
    }
    #endregion

    #region Methods
    /// <summary>
    /// Adds an element to the Start of the Array
    /// </summary>
    public void AddFirst(T data)
    {
        if (!IsFull || !_isInfinite)
        {
            if (_size != 0)
            {
                _start = GetPreviousPosition(_start);
            }
            else
            {
                _start = 0;
            }
            _array[_start] = data;
            _size++;
        }
        else if (_isDynamic)
        {
            Extend();
            AddFirst(data);
        }
        else
        {
            throw new Exception("Array is Full");
        }
    }

    /// <summary>
    /// Adds an element to the end of the Array
    /// </summary>
    public void AddLast(T data)
    {
        if (!IsFull || _isInfinite)
        {
            if (_size != 0)
            {
                _end = GetNextPosition(_end);
            }
            else
            {
                _end = 0;
            }
            _array[_end] = data;
            _size++;
        }
        else if (_isDynamic)
        {
            Extend();
            AddLast(data);
        }
        else
        {
            throw new Exception("Array is Full");
        }
    }

    /// <summary>
    /// Removes the First Element in the Array
    /// </summary>
    public void RemoveFirst()
    {
        if (!IsEmpty)
        {
            _start = GetNextPosition(_start);
            _size--;
        }
        else
        {
            throw new Exception("Array is Empty");
        }
    }

    /// <summary>
    /// Removes the Last Element in the Array
    /// </summary>
    public void RemoveLast()
    {
        if (!IsEmpty)
        {
            _end = GetPreviousPosition(_end);
            _size--;
        }
        else
        {
            throw new Exception("Array is Empty");
        }
    }

    /// <summary>
    /// Extends the Current Array
    /// </summary>
    /// <param name="array">Current Array</param>
    /// <returns>Extended Array</returns>
    private void Extend()
    {
        Extend(Capacity * 2);
    }

    /// <summary>
    /// Extends the Current Array
    /// </summary>
    /// <param name="array">Current Array</param>
    /// <returns>Extended Array</returns>
    private void Extend(Int32 capacity)
    {
        T[] extendedArray = new T[capacity];
        if (capacity < Capacity)
        {
            Int32 end = 0;
            Int32 size = 0;
            for (Int32 i = 0; i < extendedArray.Length && i < _array.Length; i++, end = GetNextPosition(end), size++)
            {
                extendedArray[i] = _array[end];
            }
            _start = 0;
            _end = --end;
            _size = size;
                
        }
        else
        {
            _array.CopyTo(extendedArray, 0);
        }
        _array = extendedArray;
    }

    /// <summary>
    /// Gets the Next Position in the Array
    /// </summary>
    /// <param name="pos">The Current Position</param>
    /// <returns>The Next Position</returns>
    private Int32 GetNextPosition(Int32 pos)
    {

        if (pos == Capacity - 1)
        {
            return 0;
        }
        else
        {
            return pos + 1;
        }
    }

    /// <summary>
    /// Gets the Previous Position in the Array
    /// </summary>
    /// <param name="pos">The Current Position</param>
    /// <returns>The Previous Position</returns>
    private Int32 GetPreviousPosition(Int32 pos)
    {
        if (pos == 0)
        {
            return Capacity - 1;
        }
        else
        {
            return pos - 1;
        }
    }

    #endregion
    #region Properties
    /// <summary>
    /// Get and Set the starting index of the Array
    /// </summary>
    protected Int32 Start
    {
        set
        {
            if (value >= 0 && value < _array.Length)
            {
                _start = value;
            }
            else
            {
                throw new Exception("Inappropriate Index");
            }
        }
        get
        {
            return _start;
        }
    }

    /// <summary>
    /// Set and Get the ending index of the Array
    /// </summary>
    protected Int32 End
    {
        set
        {
            if (value >= 0 && value < _array.Length)
            {
                _end = value;
            }
            else
            {
                throw new Exception("Inappropriate Index");
            }
        }
        get
        {
            return _end;
        }
    }

    /// <summary>
    /// Get the Number of Elements in the Array
    /// </summary>
    public Int32 Size
    {
        get
        {
            return _size;
        }
    }

    /// <summary>
    /// Get and Set Capacity of the Array
    /// </summary>
    public Int32 Capacity
    {
        set
        {
            Extend(value);
        }
        get
        {
            return _array.Length;
        }
    }

    /// <summary>
    /// Get and Set the Array to being Dynamic
    /// </summary>
    public Boolean IsDynamic
    {
        set
        {
            _isDynamic = value;
        }
        get
        {
            return _isDynamic;
        }
    }

    /// <summary>
    /// Gaet and Set the Array to being Infinite
    /// </summary>
    public Boolean IsInfinite
    {
        set
        {
            _isInfinite = value;
        }
        get
        {
            return _isInfinite;
        }
    }

    /// <summary>
    /// Is the Array Empty
    /// </summary>
    private Boolean IsEmpty
    {
        get
        {
            return _size == 0;
        }
    }

    /// <summary>
    /// Is the Array Full
    /// </summary>
    private Boolean IsFull
    {
        get
        {
            return _size == Capacity;
        }
    }
    #endregion
}
