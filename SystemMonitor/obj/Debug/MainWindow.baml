   M S B A M L   `   `   `  ����  WPFRollingMonitorbCclr-namespace:DNBSoft.WPF.RollingMonitor;assembly=WPFRollingMonitorDNBSoft.WPF.RollingMonitor  5   	   H DSystemMonitor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:SystemMonitorSystemMonitor 5   1      SystemMonitor.MainWindow   5   	   W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      8x,http://schemas.microsoft.com/winfx/2006/xaml    5      SchartCclr-namespace:DNBSoft.WPF.RollingMonitor;assembly=WPFRollingMonitor    5   	   (myclr-namespace:SystemMonitor    5   1     � Title$  
MainWindow��6   $	��565��6   $	��767��6%    � 
ResizeMode$ NoResize=�6X    � 	Resources 5         CpuTemplate � RowDefinitions%  SystemMonitor.HalfCircleControl  Name   Unit   Value��%m  &	        �� 5   
   .R�5	      � ���	#FF302E2E 5
      �� 5      $��* �6$   �� 5      $��30 �6$   .+�5        $ HalfCircleControl1��6B   $
��Left=�6'   $	��Top=�6^   $	��120��6v   $	��120��6�    6�   �� *Unit+ 6�   �� *Value+�� 5      $��1��6   
���������6%   �� *Name+6>   5      5      5      .��5      � .+�5   
     -   5        $ HalfCircleControl1��5   :   $
��Left=�6   $	��Top=�6V   $	��350��6n   $	��350��6{   $��19,51.973,0,0q�6�     5   
   -   5        $ HalfCircleControl2��5   c   $��415.027��6   $
��Left=�60   $��409,51.973,0,0q�6K   $	��Top=�6   $	��350��6�   �� 5   
   -   5        $��Label1��5   X   $
��CPU:��6   $��28��6   $
��Left=�6+   $��4,12,0,0q�6F   $	��Top=�6f    J� RenderTransformOrigin$ 
0.37,0.536&�6~   �� 5   
   -   5        $��Label2��5   Y   $	��RAM��6   $��28��6   $
��Left=�6*   $��
408,12,0,0q�6E   $	��Top=�6g   �� 5   
   -   5         ��Name$ slider1��5      $
��Left=�6"   $��165,500,0,0q�6=   $	��Top=�6R   $	��430��6j   	 � Maximum$		 100Q�6v   
 � Minimum$
 0Q�6�    � Value$ 0Q�6�   6�   �� 5   
   -   5         ��Name$ check1��5      $��Show CPU and RAM��6#   $
��Left=�6>   $��321,472,0,0q�6Y   $	��Top=�6n   �� 5   
   -   5         ��Name$ lbCPU��5      $��Label��6   $
��Left=�6/   $��33.393,12,0,0q�6J   $	��Top=�6a   $��369.607��6y   �� 5   
   -   5         ��Name$ lbCores��5      $
��Left=�6#   $��415.027��6>   $��2,51.973,0,0q�6O   $	��Top=�6e   $	��402��6}   #��[  6�   $��Disabled=�6�   ��5      �� 5      .R�5      �  � Orientation$ 
Horizontal=�6    5       5"      5#      5$      