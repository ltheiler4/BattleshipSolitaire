   M S B A M L   `   `   `  ����E  ABattleship, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Battleship.ViewBattleship.View  5   	       Battleship.View.Config   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:Battleship.View     5   	     � Title$  Config��5   	   $	��500��6   $	��500��6%   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $
��0.5* �6   6.   �� 5      $��2* �6   6,   �� 5      $
��0.1* �6   6.   �� 5      $��2* �6   6,   �� 5      $
��0.5* �6   6.    � RowDefinitions 5   
   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   .+�5   
   �� $��1��6   $��3��6    $��Center=�64   $��25(�6Q   $��Configuration��6`   �� 5   
   $��1��6   $��1��6    $��Center=�6-   $��Center=�6H   $��20(�6e   $��Anzahl Spalten:��6s   �� 5   
   $��1��6   $��2��6    $��Center=�6.   $��Center=�6I   $��20(�6f   $��Anzahl Zeilen:��6t   �� 5    
   $��1��6   $��3��6    $��Center=�6.   $��Center=�6I   $��20(�6f   $��Anzahl Schiffe 1-er:��6t   �� 5!   
   $��1��6   $��4��6    $��Center=�6.   $��Center=�6I   $��20(�6f   $��Anzahl Schiffe 2-er:��6t   �� 5"   
   $��1��6   $��5��6    $��Center=�6.   $��Center=�6I   $��20(�6f   $��Anzahl Schiffe 3-er:��6t   �� 5#   
   $��1��6   $��6��6    $��Center=�6.   $��Center=�6I   $��20(�6f   $��Anzahl Schiffe 4-er:��6t   �� 5&   
   -   5         ��Name$ ColumnSlider��5&   �   $��3��6   $��1��6!   $��10,15,10,15q�6.    � Minimum$ 6Q�6C    � Maximum$ 15Q�6O    �� TickFrequency$ 1Q�6\   $��20(�6n    �� TickPlacement$ BottomRight=�6|    �� IsSnapToTickEnabled . 	 �� AutoToolTipPlacement$	 TopLeft=�6�   
 � Value
 6�   �� *ColumnValue+�� 5'   
   -   5        $ 	RowSlider��5'   �   $��3��6   $��2��6!   $��10,15,10,15q�6.   $ 6Q�6C   $ 15Q�6O   $ 1Q�6\   $��20(�6n   $ BottomRight=�6|    . $	 TopLeft=�6�   
 6�   �� *
RowValue+�� 5(   
   -   5        $ 
einerSlide��5(   �   $��3��6   $��3��6!   $��10,15,10,15q�6.   $ 1Q�6C   $ 3Q�6O   $ 1Q�6[   $��20(�6m   $ BottomRight=�6{    . $	 TopLeft=�6�   
 6�   �� *	EinerShip+�� 5)   
   -   5        $ zweierSlide��5)   �   $��3��6   $��4��6!   $��10,15,10,15q�6.   $ 1Q�6C   $ 2Q�6O   $ 1Q�6[   $��20(�6m   $ BottomRight=�6{    . $	 TopLeft=�6�   
 6�   �� *
ZweierShip+�� 5*   
   -   5        $ dreierSlide��5*   �   $��3��6   $��5��6!   $��10,15,10,15q�6.   $ 1Q�6C   $ 2Q�6O   $ 1Q�6[   $��20(�6m   $ BottomRight=�6{    . $	 TopLeft=�6�   
 6�   �� *
DreierShip+�� 5+   
   -   5        $ viererSlide��5+   �   $��3��6   $��6��6!   $��10,15,10,15q�6.   $ 1Q�6C   $ 2Q�6O   $ 1Q�6[   $��20(�6m   $ BottomRight=�6{    . $	 TopLeft=�6�   
 6�   �� *
ViererShip+�� 5,   
   $��3��6   $��7��6!   $��30,10,30,10q�6.   $��20(�6C   $
��Save��6R   ��6a   �� *RelayCommand+��6�   ��  �� RelativeSource �� *Self+6�   5-      5.      