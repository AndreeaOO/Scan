Table table;
long code, barcode=0;
String brand, product;
String list = "";
String barcodeBuffer = "";
String[] split_list;

void setup() {
  size(200, 200);
   
  table = loadTable("full_items.csv", "header");

  println(table.getRowCount() + " rows in table"); 

  for (TableRow row : table.rows()) {
    
     code = row.getLong("Code");
     brand = row.getString("Brand");
     product = row.getString("Product");
    
    println("Code:" + code + " (Brand name:" + brand + ", Product name:" + product + ")");
      
  }

}


void draw(){}

void keyPressed()
{
  //println("pressed " + key);
   
  if((byte)key != 10)
  {   
      barcodeBuffer = barcodeBuffer + key;
  }
  else
  {
    barcode = java.lang.Long.parseLong(barcodeBuffer.trim());
    println("new barcode: " + barcode);    
    barcodeBuffer = "";
    
    if (barcode == code )
    {
      list = list + " " + product;
      println("in the fridge:" + list);     
    }
    else
    {
      println("insert product name");
      list = list + " ";
    }
  }
  split_list = split(list, ' ');
  saveStrings("list.txt", split_list);
 
  
}