   M S B A M L   `   `   `  ����I  EAirQualittyApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:AirQualittyApp.ViewModelsAirQualittyApp.ViewModels  5   	   /clr-namespace:AirQualittyAppAirQualittyApp  5   	       AirQualittyApp.MainPage   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6vm'clr-namespace:AirQualittyApp.ViewModels     5   	   .localclr-namespace:AirQualittyApp     5   	     � Title$  Air Quality��5	   	   $	��580��6   $	��899��6*   .��5      � 
�������.+�5   
   ��  �� Orientation$ Vertical=�6   $
��Left=�6,   $	��650��6G   $	��550��6S   $	��Top=�6`   
�������.+�5      �� $ 
Horizontal=�6   .+�5      �� $	��346��6f   $��35��6r   $��20(�6~   $��Calibri)�6�   $��Bottom=�6�   ���	#FFDCECFA��6   �� *
CityName+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�.��6�   ��5      �� 5      .��5   %   5      �� 5      $��55��6   $��35��6$   $�� 30, 0, 0, 0q�60   $��Bottom=�6F   ���	#FFDCECFA��6y   �� *SearchCommand+.��5      �� $��Assets\Find.png��6   $��29��65   $��26��6A   6N   5      5      �� 5      $��
0, 0, 0, 0q�6   $	��650��6)   $	��465��65   ��6B   ��  �� Path$ ColorForCanvas�.+�5      �� $��60��6   $��94��6(    �� Left$	 320��63   
 �� Top$	 226��6E   $��40(�6V   ��6d   �� *	Quality+$ OneWay=�$ PropertyChanged=�5      �� 5      $��Assets\Scale_.png��6   $��
0, 5, 0, 0q�6/   $	��614��6C   $��33��6O   $��Bottom=�6[   6y   5      �� 5    
   $	��248��6   $ Vertical=�6"   $
��Left=�69   $��	651,0,0,0q�6T   
�������.+�5"      �� $��35��6   $��Calibri)�6&   $��20(�6;    �� Padding$ 
0, 5, 0, 0q�6J   $��Center=�6_   ���	#FFDCECFA$	��250��6�   $
��Wrap=�6�   " Select cities to get statistics:6�   �� 5#      $	��465��65   ��6   �� *Cities+��5$      �� 5%      .R�5&      �� $��5q�6%   $ 
Horizontal=�60   .+�5'      �� 
����������	#FF040404 d� 	IsChecked 6V   �� *	IsChecked+��6v   �� $
 Name�6�   5(      5)      5+      �� 5-      $��105, 5, 0, 0q�6   $��35��6+   $	��112��67   $��Go to statistic��6C   $��16(�6]   $��Calibri)�6k   ���	#FFDCECFA��6�   �� *StatisticCommand+6�   5.      51      52      