   M S B A M L   `   `   `  ���� 
  MyFire    MyFire.TweetDisplayDialog   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006       � Title$  myFire | Viewing a tweet.�� � WindowStartupLocation$ CenterScreen=� J� Opacity$ 1Q� � WindowStyle$ SingleBorderWindow=� � Icon$5 //MyFire;component/Resources/Retro-Fire-Ball.ico�� � SizeToContent$ WidthAndHeight=� � 
ResizeMode$ NoResize=�$��0,0,0,10q�$	��400��$	��300��$	��200��.��� .+��� .+�� -   $��UserGrid�� � Cursor$
 Hand������ -   UserBackground�� j� Stretch$ UniformToFill=�.+��� $	��1.5q�$
��Left=�$��10q�$��50��$��50��._��� -   	 ��Name$	 TweetUserImage��
 J� Effect
 3  -System.Windows.Media.Effects.DropShadowEffect     Color$ Black��   	Direction$ 0Q�   ShadowDepth$ 0Q��� -   $��	user_name��$��null:screenname��$��20(�
�������$
��Left=�$��Center=�$��67,0,0,0q�
   $ Black��$ 0Q�$ 0Q��� $��13��$-��'/MyFire;component/Images/view_arrow.png��$
��Fill=�$��9��$��Stretch=�$��Right=�$��5q�
   $ Black��$ 0Q�$ 0Q�� -   $��	TweetGrid��.+��� .+��� -   $��	TweetText��$��5q�$��14(�$��0q� �� 
IsReadOnly . �� -   $��MediaBorder��$��50��$��50��$
��Left=�$��5,0,0,0q�$��1q�
�������$y�	Collapsed=�
   $ Black��$ 0Q�$ 0Q�._��� -   $��
MediaImage��$��UniformToFill=��� $��1��
�������$	��0,5q���  �� Orientation$ 
Horizontal=�$��Center=�.+��� -	   $��	TweetDate��$��null:tweet.date��$��5,0,0,0q��� $��1��
�������$	��5,0q��� -
   $��TweetID��$��null:tweet.id��$��0,0,5,0q��� $��1��
�������$	��0,5q��� $ 
Horizontal=�$��Center=�.+��� -   $��MenuBtn��$
��Menu��$	��2.5q� � ContextMenu �� -   $��MenuCM��.@�m� $��Tweet��.@�m� -   $��	Favourite��m� $��Retweet��.@�m� -   $��Twitter style��m� -   $��Quote tweet��m� -   $��	Translate��m� $
��User��.@�m� -   $��Report���� -   $��InReplyToBtn��$��In Reply To...��$	��2.5q��� -   $��LocationBtn��$��Tweet Location��$	��2.5q�