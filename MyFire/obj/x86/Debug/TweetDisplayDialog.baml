   M S B A M L   `   `   `  ����
  MyFire    MyFire.TweetDisplayDialog   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5   �   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6�     � Title$  myFire | Viewing a tweet.��6    � WindowStartupLocation$ CenterScreen=�6'    J� Opacity$ 1Q�6L    � WindowStyle$ SingleBorderWindow=�6X    � Icon$5 //MyFire;component/Resources/Retro-Fire-Ball.ico��6y    � SizeToContent$ WidthAndHeight=�6E   � 
ResizeMode$ NoResize=�6d  $��0,0,0,10q�6z  $	��400��6�  $	��300��6�  $	��200��6�  .��5      � .+�5   
   �� .+�5      � -   5        $��UserGrid��5       � Cursor$
 Hand��6#   ��5      �� 5	      -   5        UserBackground�� j� Stretch$ UniformToFill=�5	   9   .+�5      �� $	��1.5q�6   $
��Left=�6/   $��10q�6J   $��50��6V   $��50��6a   ._�5      �� -   5        	 ��Name$	 TweetUserImage��5      
 J� Effect
 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         Color$ Black��6/      	Direction$ 0Q�6=      ShadowDepth$ 0Q�6K   5      5      �� 5      -   5        $��	user_name��5   A   $��null:screenname��6   $��20(�63   
�������$
��Left=�6e   $��Center=�6�   $��67,0,0,0q�6�   
 6�     6�   $ Black��6�   $ 0Q�6�   $ 0Q�6�   6  �� 5      $��13��6   $-��'/MyFire;component/Images/view_arrow.png��6$   $
��Fill=�6U   $��9��6d   $��Stretch=�6n   $��Right=�6�   $��5q�6�   
 6�     6�   $ Black��6�   $ 0Q�6�   $ 0Q�6�   5      5      � 5      -   5        $��	TweetGrid��5      .+�5      �� .+�5      �� -   5        $��	TweetText��5   "   $��5q�63   $��14(�6>   $��0q�6L    �� 
IsReadOnly . 6`   �� 5      -   5        $��MediaBorder��5      $��50��60   $��50��6<   $
��Left=�6G   $��5,0,0,0q�6b   $��1q�6s   
�������$y�	Collapsed=�6�   
 6�     6�   $ Black��6�   $ 0Q�6�   $ 0Q�6�   ._�5      �� -   5        $��
MediaImage��5       $��UniformToFill=�62   5      �� 5      $��1��6    
�������$	��0,5q�67   �� 5       �� Orientation$ 
Horizontal=�6!   $��Center=�6:   .+�5      �� -	   5        $��	TweetDate��5   $   $��null:tweet.date��65   $��5,0,0,0q�6L   �� 5      $��1��6$   
�������$	��5,0q�6:   �� 5       -
   5        $��TweetID��5    $   $��null:tweet.id��63   $��0,0,5,0q�6H   5!      �� 5"      $��1��6    
�������$	��0,5q�67   �� 5#      $ 
Horizontal=�6!   $��Center=�6:   .+�5$      �� -   5        $��MenuBtn��5$   =   $
��Menu��6!   $	��2.5q�60    � ContextMenu 5%      �� 5&   "   -   5        $��MenuCM��5&   .   .@�5'   &   m� $��Tweet��6/   .@�5(   *   m� -   5        $��	Favourite��5(   3   6F   m� 5)   *   $��Retweet��63   .@�5*   .   m� -   5        $��Twitter style��5*   7   6N   m� 5+   .   -   5        $��Quote tweet��5+   7   6L   5,   +   m� 5-   *   -   5        $��	Translate��5-   3   6F   5.   '   m� 5/   &   $
��User��6/   .@�50   *   m� -   5        $��Report��50   3   6C   51   '   52   #   54      �� 55      -   5        $��InReplyToBtn��55   G   $��In Reply To...��6!   $	��2.5q�6:   6G   �� 56      -   5        $��LocationBtn��56   G   $��Tweet Location��6!   $	��2.5q�6:   6G   57      58      59      5:      5;      5<      