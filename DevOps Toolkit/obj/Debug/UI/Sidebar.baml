   M S B A M L   `   `   `  ����I  EDevOps Toolkit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:DevOps_Toolkit.UIDevOps_Toolkit.UI  5          DevOps_Toolkit.UI.Sidebar   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:DevOps_Toolkit.UI     5        � 	Resources  5	         I_Item_Default �� 	UriSource  C_BackgroundColor $  C_Item_IsSelectedBackgroundColor  C_Item_IsSelected  C_Item_HoverColor��%  &	        &	 W     &	 v     &	 �     &	 �     �� 5
      $C =/DevOps Toolkit;component/Images/Dark/appbar.control.stop.png?�6'   ��5      #29384f��67   ��5      #3e487f��6F   ��5      #9fa4fe��67   ��5      #4c519c��67   5      .��5      �  � ColumnDefinitions 5      �� 5      $��85 �5      �� 5      $��* �5      .+�5      � $��0��5      #��[ 5      .+�5      �� -   5         ��Name$ Content��5      .+�5      �� $��0��66   ��6   �� *	registrar+��5      �� 5      .R�5   	   � $��67��6   ��5   
   �� 5        �� 
TargetType" �6   .�5!      ��  �� Property � �� �� Value# [ 6)   6V    �� Triggers 5"      �� 5#       �� Binding 6   �� *
isSelected+	 �� Value	 True69   .a�5$      ��  � ��# [ 6+   5%      P� 5&      
 P� Property
 �@J�IsMouseOver P� Value . . �5'      ��  � ��# [ 6+   5(      5*      .+�5,   
   � $��5��6   #��[ 6   $
��Left=�6I   ��5-      �� 5.      " �6    5/      �� 50       6   �� *
isSelected+		 False6:   .a�51      ��  � y�$ Hidden=�6,   52      54      56      �� 57   
   ��6   �� *Icon+6*   � 58   
   -   5        
������ 58   U   59   
   5:   	   5<      5=      5?      5@      5A      