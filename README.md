# APS(高级计划与排程/排产)
## 建模
1. MtrlObject 物料/产品
2. BOMObject 物料清单 主键(主物料、清单号)
3. BOMItemObject 物料清单明细 主键(主物料、清单号、子物料)
4. TechStepObject 工艺路线 主键(物料、序号) 树型结构 描述部件生产的主要步骤
5. ScObject 分部
6. WkpObject 车间
7. WrkgrpObject 工组
8. ResourceObject 资源 包含工人、设备、模具
9. ResourceTypeObject 资源分类 如钻孔组工人、钻孔组排钻、钻孔组A模具
10. ResourceMap 资源分类关联 因为一个具体资源可能会属于多个分类
11. TechItemRsType 工艺路线需要使用的资源如 钻孔组工人 2人、钻孔组排钻 1台、钻孔组A模具 1副
12. TechItemObject 工艺路线步骤可选方案 与机器抛光与人工抛光
13. ResourceDateDef 资源日历定义，也是生成资源日历的策略
14. ResourceDate 资源日历 记录资源在具体时间里的状态，如开始时间、结束时间、已占用时间、被哪些资源占用
15. MLObject 主计划 也是产品需求
16. ZLObject 指令单 MRP运算所得的自制需求
17. RqMtrlTreeObject 主计划的物料运算结果 树型结构
18. RqMtrlObject 主计划物料需求汇总平铺结构
19. TreeObject 工艺路线实例关联指令单
20. TreeDateObject 工艺路线占用日历明细，占用了某资源日历多少时间
21. MtrlGroup 物料成组，主键(组号、物料)一种物料只可以属性一个组。这里成组的意义在于相同型号的产品，其中只有一些部件不相同，相同部分一起生产。特别是使用相同设备与模具的部件，一起生产可以减少了换模的时间
22. TechItemGroup 工艺成组，使用相同设备与模具的工艺可以成组排产
23. MLGroup 主计划成组，小批量的主计划合并排产
