   M S B A M L   `   `   `  ����O  KStopify.Presentation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulla5clr-namespace:Stopify.Presentation.Views.PlaylistView'Stopify.Presentation.Views.PlaylistView  5      >    8Stopify.Presentation.Views.PlaylistView.PlaylistViewMain   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Glocal5clr-namespace:Stopify.Presentation.Views.PlaylistView     5      -   5          N� Title$  PlaylistView��5      .��5      ��  �� CornerRadius$ 8��6   ._�5   
   � .+�5      � -   5        $��StickyHeader��5   $    K� ZIndex$ 1��6   $��0��68    � RowDefinitions 5      �� 5      $
��Auto �6$   �� 5      $
��Auto �6$   .+�5      �� -   5        $��StickyHeaderBg��5      $��0��5      
������ $ 8 8 0 0��5      ._�5      �  � ColumnDefinitions 5      �� 5      $
��Auto �6/   �� 5       $��* �6/   .+�5$      �� $��0��5%      
������ $��0q�5'      $��50��5(      $��Stretch=�5*      $��Stretch=�5+      $��15 7 7 7q�5,      ��5)      �� *ActualWidth+ �� RelativeSource �� *Self+.��5.      �� $ 30��6%   ���#1ed760._�5/   "   �� $0��*../../Resources/#Font Awesome 6 Free Solid)�50   %   $	����51   %   $��	2 15 0 15q�52   %   $��19(�53   %   $��Center=�54   %   $��Center=�55   %   57      58      �� 5;      $��1��5<      
�������$
��Wrap=�5>      $(��"Coding Music  Programming Playlist��5?      $
��Bold"�5@      $��24(�5A      $��35��5B       �� LineStackingStrategy$ BlockLineHeight=�5C      $��CharacterEllipsis=�5D      $��Center=�5E      $��0 0 20 0q�5F      5H      5I      @   :Stopify.Presentation.Views.PlaylistView.PlaylistViewHeader  5K      $��1��6+   ���#1f1f1f68   5L      �� 5O      -   5        $��Scroller��5P      $ 0��5Q      $
��Auto=�5R      .��5T      � ��5U      �� 5V       �� 
StartPoint$	 0,0&�6.    �� EndPoint$	 0,1&�6?   .��5W      � -   5        
ScrollerBg��	 � Color$	 Transparent��5W   ?   
 � Offset$	
 .25Q�6S   � 5X      $	 #121212��6+   $	
 .65Q�6;   5Y       5\      �� 5]      $
��Auto �6(   �� 5^      $
��Auto �6(   �� 5_      $
��Auto �6(   �� 5`      $
��Auto �6(   �� 5a      $
��Auto �6(   .+�5e      � -   5        $��
MainHeader��5g      $��0��5f      $	��Top=�5h      
������ $	��187��5j       5k      �� 5l      $
��.32* �6/   $	��160��6<   �� 5m      $��* �6/   .+�5q      �� $��0��5r      $ 4��5s      $	��126��5t      $	��170��5u      $��
17 0 17 18q�5w      $��Bottom=�5x      ��5v      �� *ActualWidth+ �� *Self+��5z      �� 5{   "    �� ImageSource$ ../../Resources/song.jpg��6-   5}      � 5�      $��1��5�      $��	0 0 17 17q�5�      $��Bottom=�5�      .+�5�      �  5�   "   �� 5�   &   $
��Auto �64   �� 5�   &   $
��Auto �64   �� 5�   &   $
��Auto �64   �� 5�   &   $
��Auto �64   .+�5�   "   �� $��0��5�   %   
�������$��Public Playlist��5�   %   $��SemiBold"�5�   %   $
��13.5(�5�   %   $��0 -1 0 3q�5�   %   �� 5�   "   -   5        $��PlaylistTitle��5�   %   $��1��5�   %   
�������$
��Wrap=�5�   %   $(��"Coding Music  Programming Playlist��5�   %   $��	UltraBold"�5�   %   $��23(�5�   %   $��WordEllipsis=�5�   %   �� 5�   "   $��2��5�   %   
�������$��SemiBold"�5�   %   $��14(�5�   %   $
��Wrap=�5�   %    �� 
LineHeight$ 18��5�   %   $��19��5�   %   $��CharacterEllipsis=�5�   %   $A��;best coding music - best coding songs - best coding chill -��5�   %   � 5�   "   $��3��6,   $��0 9 0 0q�69   .+�5�   &   ��  �� Orientation$ 
Horizontal=�61   .+�5�   *   �� $ 30��5�   -   $��20��5�   -   $��20��68   $��0 0 4 0q�5�   -   ��5�   .   �� 5�   2   $&  ../../Resources/stopify-icon.ico��6=   5�   +   �� 5�   *   -   5        $��HeaderAuthor��5�   -   
������ $��0q�5�   -   
�������
�������$
��Bold"�5�   -   $��14(�5�   -   $��0q�5�   -   $��0 0 0 1q�5�   -   $��Azahriah��5�   -   5�   -   5�   '   �� 5�   &   $ 
Horizontal=�61   .+�5�   *   �� $��·��5�   -   
�������$
��Bold"�5�   -   $��20(�5�   -   $��18��5�   -   $��5 -9 5 0q�5�   -   �� 5�   *   
�������$��SemiBold"�5�   -   $��14(�5�   -   $��97,791��5�   -   �� 5�   *   $��saves��5�   -   
�������$��SemiBold"�5�   -   $��14(�5�   -   $��4 0 0 0q�5�   -   5�   '   �� 5�   &   $ 
Horizontal=�61   .+�5�   *   �� $��·��5�   -   
�������$
��Bold"�5�   -   $��20(�5�   -   $��18��5�   -   $��5 -9 5 0q�5�   -   �� 5�   *   
�������$��SemiBold"�5�   -   $��14(�5�   -   $	��200��5�   -   �� 5�   *   $��songs, ��5�   -   
�������$��SemiBold"�5�   -   $��14(�5�   -   $��4 0 0 0q�5�   -   �� 5   *   
�������$��SemiBold"�5  -   $��14(�5  -   $��7 hr 16 min��5  -   5  '   5  #   5     5	     5
     � 5     $��1��6   $��	-2 14 0 0q�6(    5     �� 5     $
��Auto �6/   �� 5     $��* �6/   �� 5     $
��Auto �6/   .+�5     � $��0��5     $
��Left=�5     $��17 0 3 0q�5      5     �� 5  "   $
��Auto �63   �� 5  "   $
��Auto �63   �� 5  "   $
��Auto �63   �� 5  "   $
��Auto �63   �� 5  "   $
��Auto �63   .+�5"     �� -	   5        $��PlayBtn��5$  !   $��0��5#  !   
������ $��0q�5&  !   $��50��5'  !   $��Stretch=�5)  !   $��Stretch=�5*  !   ��5(  !   �� *ActualWidth+ �� *Self+.��5/  "   �� -
   5        $��
PlayBorder��5/  )   ���#1ed760$ 30��6P   ._�50  &   �� -   5        $��PlayIcon��51  )   $0��*../../Resources/#Font Awesome 6 Free Solid)�52  )   $	����53  )   $��	2 15 0 15q�54  )   $��20(�55  )   $��Center=�56  )   $��Center=�57  )    J� IsHitTestVisible$ False��58  )   5:  #   5;     �� 5>     -   5        $��
ShuffleBtn��5@  !   $��1��5?  !   $0��*../../Resources/#Font Awesome 6 Free Solid)�5A  !   $	����5B  !   $��23(�5C  !   $��0q�5D  !   
������ 
�������$��32��5G  !   $��32��5H  !   $��19 0 0 0q�5I  !   5L  !   �� 5P     -   5        $��SaveBtn��5R  !   $��2��5Q  !   
������ $��0q�5T  !   $��24��5U  !   $
��12 0q�5V  !   $��Stretch=�5X  !   $��Stretch=�5Y  !   ��5W  !   �� *ActualWidth+ �� *Self+.��5]  "   �� -   5        $��
SaveBorder��5^  %   
������ $ 30��5`  %   
�������$��2q�5b  %   ._�5c  &   �� -   5        $��SaveText��5d  )   $0��*../../Resources/#Font Awesome 6 Free Solid)�5e  )   $��+��5f  )   
�������$��5q�5h  )   $��15(�5i  )   $��Center=�5j  )   $��Center=�5k  )   5m  #   5n     �� 5q     -   5        $��DownloadBtn��5s  !   $��3��5r  !   
������ $��0q�5u  !   $��24��5v  !   $	��4 0q�5w  !   $��Stretch=�5y  !   $��Stretch=�5z  !   ��5x  !   �� *ActualWidth+ �� *Self+.��5~  "   �� -   5        $��DownloadBorder��5  %   $ 30��5�  %   $��2q�5�  %   
�������._�5�  &   �� -   5        $��DownloadText��5�  )   $0��*../../Resources/#Font Awesome 6 Free Solid)�5�  )   $	����5�  )   
�������$��13(�5�  )   $��Center=�5�  )   $��Center=�5�  )   5�  #   5�     �� 5�     -   5        $��
OptionsBtn��5�  !   $��4��5�  !   $��• • •��5�  !   $��12(�5�  !   $��0q�5�  !   
������ 
�������$��25��5�  !   $��45��5�  !   $��12 0 0 1q�5�  !   5�  !   5�     � 5�     $��1��6   $��Right=�6/    5�     �� 5�  "   $
��Auto �63   �� 5�  "   $��* �63   .+�5�     �� -   5        $��	SearchBtn��5�  !   $��0��5�  !   
������ $��0q�5�  !   $
��Left=�5�  !   $��Center=�5�  !   $��Stretch=�5�  !   $��Stretch=�5�  !   $��32��5�  !   $��32��6,   .��5�  "   �� -   5        $��SearchBorder��5�  )   ���#121212$ 30��6R   ._�5�  &   �� -   5        $��
SearchText��5�  )   $0��*../../Resources/#Font Awesome 6 Free Solid)�5�  )   $	����5�  )   $��Center=�5�  )   $��Center=�5�  )   $��15(�5�  )   
�������5�  )   5�  #   5�     �� 5�     -   5        $��	SearchBar��5�  !   $��1��5�  !   $��0��5�  !   ���#272727$ 0 5 5 0��5�  !   $��
-1 10 0 10q�5�  !   ._�5�  "   �  5�  &   �� 5�  *   $��* �6;   �� 5�  *   $
��Auto �6;   .+�5�  &   �� -   5        $��	SearchBox��5�  )   $��0��5�  )   
������ $��0q�5�  )   
�������$��SemiBold"�5�  )   $��Center=�5�  )   $��14(�5�  )   $ 1��5�  )   5�  )   �� 5�  &   -   5        $��SearchbarText��5�  )   $��0��5�  )   $��Search in playlist��5�  )   
�������$��SemiBold"�5�  )   $��Center=�5�  )   $��14(�5�  )   $��2 0 0 0q�5�  )   $ 0��5�  )   �� 5�  &   -   5        $��SearchCloseBtn��5�  )   $��1��5�  )   
������ $��0q�5�  )   $	��✕��5�  )   
�������$��SemiBold"�5�  )   $��14(�5�  )   $	��7 0q�5�  )   $ 2��5�  )   5�  )   5�  #   5�     5�     � 5      $��2��6   $��Right=�6/   $��7 0 17 0q�6K    5     �� 5  "   $
��Auto �63   �� 5  "   $
��Auto �63   .+�5     �� -   5        $��CustomOrderBtn��5  !   $��1��5  !   
������ $��0q�5
  !   $��Right=�5  !   $��Center=�5  !   $	��0 5q�5  !   .��5  "   �� $ 
Horizontal=�6-   .+�5  &   �� -   5        $��CustomOrderFilterText��5  )   $
��Auto��5  )   $��Custom order��5  )   $��SemiBold"�5  )   
�������$��14(�5  )    �� Padding$ 8 0 0 2q�5  )   �� 5  &   -   5        $��CustomOrderFilterIcon��5  )   $0��*../../Resources/#Font Awesome 6 Free Solid)�5  )   $��Center=�5  )   $	����5   )   
�������$��16(�5"  )   $	 8 0q�5#  )   5%  #   5&     5'     5(       5+     $��2��6/   $��0 16 0 0q�6<   �� 5.     $��3��6#   $��16 7 0 7q�60   .@�5/     >   8Stopify.Presentation.Views.PlaylistView.PlaylistViewItem  50     � 53     $��4��6   $��
16 20 0 20q�6(    54     �� 55     $
��Auto �6,   �� 56     $
��Auto �6,   �� 57     $
��Auto �6,   �� 58     $
��Auto �6,   .+�5;     �� $��0��5<     $��Recommended��5=     $
��Bold"�5>     
�������$��25(�5@     �� 5C     $��1��5D     $&�� Based on what's in this playlist��5E     $��SemiBold"�5F     
�������$��14(�5H     $	��0 5q�5I     �� 5L     $��2��6'   $
��0 15q�64   .@�5M       5N     �� 5P     -   5        $��
RefreshBtn��5R     $��3��5Q     
������ $��0q�5T     $��Refresh��5U     $
��Bold"�5V     
�������$��Right=�5X     $��17(�5Y     $��25 10q�5Z     $	��5 0q�5[     5^     5`     5a     5b     5c     5d     5e     