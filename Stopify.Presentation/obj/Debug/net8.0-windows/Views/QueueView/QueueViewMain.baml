   M S B A M L   `   `   `  ����O  KStopify.Presentation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null[2clr-namespace:Stopify.Presentation.Views.QueueView$Stopify.Presentation.Views.QueueView  5      e7clr-namespace:Stopify.Presentation.Views.NowPlayingView)Stopify.Presentation.Views.NowPlayingView  5      8    2Stopify.Presentation.Views.QueueView.QueueViewMain   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Dlocal2clr-namespace:Stopify.Presentation.Views.QueueView     5      N
nowPlaying7clr-namespace:Stopify.Presentation.Views.NowPlayingView     5      .��5	      � $	��Top=�6     � RowDefinitions  5
   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   � $��0��6   $��8q�6    � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $
��Auto �6#   �� 5      $��* �6#   .+�5      �� -   5        $��QueueBtn��5      $��0��5      
������ ���#1ed760$��0 0 0 2q�5      
�������$��Queue��5      $
��Bold"�5       $��14(�5!      $��
12 14 12 7q�5"      $��0 0 1 0q�5#      5&      �� 5*      -   5        $��	RecentBtn��5,      $��1��5+      
������ 
������ $��0 0 0 2q�5/      
�������$��Recently played��51      $
��Bold"�52      $��14(�53      $��
12 14 12 7q�54      57      �� 5;      -   5        $��CloseBtn��5=      $��2��5<      
������ $��0q�5?      $��Stretch=�5@      $��Stretch=�5A      $��2 4 9 0q�5B      $��Right=�5C      .��5G      �� -   5        $��CloseBorder��5H       �� CornerRadius$ 30��5I      ���#121212$��29��5K      ��5L      �� *ActualWidth+ �� RelativeSource �� *Self+._�5M      �� -   5        $��	CloseText��5N      $	��✕��5O      $��Center=�5P      $��Center=�5Q      
�������$��SemiBold"�5S      $��15(�5T      $��0 0 1 1q�5U      5W      5X      5Y      �� 5\   
   $��1��6   $��0 0 0 13q�6$   .��5]      �� .+�5_      �� -   5        $��
NowPlaying��5_      $
��Auto��6/   $
��0 28q�6=   .+�5`      �� $��Now playing��5a      
�������$
��Bold"�5c      $
��15.5(�5d      $��15 0 0 5q�5e      B   <Stopify.Presentation.Views.NowPlayingView.NowPlayingViewItem  5g      5h      �� 5k      .+�5l      �� -   5        $��NextFrom��5l   !    �� Orientation$ 
Horizontal=�61   .+�5m      �� $��
Next from:��5n      
�������$
��Bold"�5p      $
��15.5(�5q      $��15 0 0 5q�5r      �� 5t      -   5        $��NextFromArtistBtn��5u      
������ $��0q�5w      $��0q�5x      
�������$
��Bold"�5z      $
��15.5(�5{      $��5 0 0 5q�5|      .��5�      �� -	   5        $��NextFromArtistText��5�   !   $��Azahriah��5�   !   $
��Wrap=�5�   !   $��CharacterEllipsis=�5�   !   $��20��5�   !   5�      5�        5�        5�        5�        5�        5�        5�        5�        5�        5�        5�      5�      5�      5�      5�      5�      