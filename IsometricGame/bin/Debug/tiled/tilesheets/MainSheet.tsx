<?xml version="1.0" encoding="UTF-8"?>
<tileset version="1.5" tiledversion="1.7.2" name="MainSheet" tilewidth="64" tileheight="64" tilecount="104" columns="13">
 <image source="../../mario clone pack or sumthin/Tilesheet/sokoban_tilesheet.png" width="832" height="512"/>
 <tile id="0">
  <animation>
   <frame tileid="92" duration="100"/>
   <frame tileid="93" duration="200"/>
   <frame tileid="91" duration="100"/>
   <frame tileid="92" duration="100"/>
  </animation>
 </tile>
 <tile id="3" probability="100"/>
 <tile id="14" type="WalkthroughCrate"/>
 <tile id="15" type="WalkthroughRed">
  <objectgroup draworder="index" id="2">
   <object id="2" x="29.3333" y="31.3333" width="34.6667" height="32.6667"/>
  </objectgroup>
 </tile>
 <tile id="16" type="WalkthroughBlue"/>
 <tile id="17" type="WalkthroughGreen"/>
 <tile id="18" type="WalkthroughGrey"/>
 <tile id="29" probability="10"/>
</tileset>
