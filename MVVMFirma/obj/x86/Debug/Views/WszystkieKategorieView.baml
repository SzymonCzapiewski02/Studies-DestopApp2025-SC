   M S B A M L   `   `   `  ����D  @MVVMFirma, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:MVVMFirma.ViewsMVVMFirma.Views  5      ,    &MVVMFirma.Views.WszystkieKategorieView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:MVVMFirma.Views     5        � 	Resources  5	       �� MergedDictionaries �� Source�� 5
   
    5      �� 5      $! StyleWszystkieViewBase.xaml?�6%   %    5      .��5      � 
�������.+�5   
   &   System.Windows.Controls.DataGrid     AutoGenerateColumns .     
DataHeader   ColumnHeaderStyle# [  5         
IsReadOnly . ��5      �� *List+ �� SelectedItem 5      ��  �� Path$ WybranyKategorie� �� Mode$ TwoWay=�	   Columns	 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn
   Header$
 Id��6%     
TextBlockS1  +System.Windows.Controls.DataGridBoundColumn   ElementStyle# [ 61      Binding 6\   �� *IdKategorii+  5      $
 Nazwa��6%   # [ 64    6_   �� *Nazwa+  5      $

 Opis��6%   # [ 63    6^   �� *Opis+5      5      5      