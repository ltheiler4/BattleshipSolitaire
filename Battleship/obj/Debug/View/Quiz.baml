   M S B A M L   `   `   `  ����E  ABattleship, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Battleship.ViewBattleship.View  5   	       Battleship.View.Quiz   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:Battleship.View     5   	     � Title$
  Quiz��5   	   $	��500��6   $	��500��6"    � SizeToContent$ WidthAndHeight=�6/    � 
ResizeMode$ NoResize=�6N   .��5	      �  J� RenderTransformOrigin$ 
0.85,0.495&�6    � ColumnDefinitions 5
   
   �� 5      6!   �� 5      $��0.25* �6   6/    � RowDefinitions 5   
   �� 5      $	��15* �6   6+   �� 5      $	��20* �6   6,   �� 5      $
��200* �6   6,   .+�5   
   �� -   5         ��Name$ export��5      $��	0,0,100,0q�6!   $��Right=�64   $��90��6P   $��Export��6�   ��6[   �� *RelayCommand+��6|   ��  �� RelativeSource �� *Self+6�   �� 5   
   -   5        $ hilfe��5      $��0,0,5,0q�6    $��90��61   $��Right=�6<   $��Hilfe��6�   ��6X   �� *RelayCommand+��6y   ��  �� *Self+6�   �� 5   
   -   5        $ correct��5      $��	0,0,195,0q�6"   $��90��65   $��Right=�6@   $��Korrigieren��6�   ��6\   �� *RelayCommand+��6}   ��  �� *Self+6�   �� 5   
   $��1��6   ��6&   �� *ColumnNumberGrid+6N   �� 5   
   $��1��6   $��2��6)   ��66   �� *RowNumberGrid+6Z   �� 5   
   $��2��6   ��6&   �� *Grid+6A   5      5      