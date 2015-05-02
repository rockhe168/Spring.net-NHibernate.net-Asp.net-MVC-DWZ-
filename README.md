# Spring.net+NHibernate.net+ Asp.net or Asp.netMVC + DWZ
基于Spring.net + NHiberNate +Asp.net MVC or Asp.net +DWZ 构建的一套后台管理系统框架


框架优点

1、利用DWZ作为UI，简化Ajax的开发

2、简化DAO层

3、此架构基于NHibernate+Spring.net开发，成熟，稳定

4、针对DB增加一列、修改一列修改代码的痛苦再也不会有了、他是针对ORM。还不是MRO

5、Service层的加入是让Dao层保持颗粒化，让Service的一个方法变成一个事务
   当然如果不加入Service层，则事务控制就转移给了DAO层


-----------------------改动------------------------------------------------------>

时间：2012-8-30
   
   在Bll层，Dao层加入工厂模式，让所有对象利用工厂统一管理
   
          优点：减少配置
          
          缺点：所有对象一下全部实例化（采用单列模式）

里面Demo 包含 asp.net，asp.net mvc也有一套

其中

RockFrameWork --基础框架代码

RockFrameWorkTemplate --框架实现，简易Demo，其中包含asp.net mvc, asp.net webform 两种实现
