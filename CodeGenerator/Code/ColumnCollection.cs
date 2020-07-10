using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class ColumnCollection: System.Collections.CollectionBase
{
    //Public functions/////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Add new column to column collection
    /// </summary>
    /// <param name="objColumn">Column object which will add</param>
    public void Add(Column objColumn)
    {
            
    }

    /// <summary>
    /// Delete column object from column collection
    /// </summary>
    /// <param name="objColumn">Column object which remove from list</param>
    /// <returns></returns>
    public Column Remove(Column objColumn)
    {
        return objColumn;;
    }

    /// <summary>
    /// Replace given column to some other column
    /// </summary>
    /// <param name="objColumn1">Column object which will replaced by other column</param>
    /// <param name="objColumn2">Column object which will replace the other column</param>
    /// <returns></returns>
    public Column Replace(Column objColumn1, Column objColumn2)
    {
        return objColumn2;
    }

    /// <summary>
    /// This function add the new column object at given index location
    /// </summary>
    /// <param name="objColumn">Column object which added</param>
    /// <param name="intIndex">Position of the new object</param>
    public void AddAt(Column objColumn, int intIndex)
    {
 
    }

    /// <summary>
    /// This function will remove the object from given index
    /// </summary>
    /// <param name="intIndex">Index of the list from which position</param>
    /// <returns></returns>
    public Column RemoveFrom(int intIndex)
    {
        return null;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////

    //Public properties
    ///ItemProperty with two type of parameters
    ///1)Index of the column
    ///2)Name of the column
    ///////////////////////////////////////////////////////////////////////////////////////////
}
