# Unity_Helper_UGUICodeGenerator
# UGUI组件自动生成代码

## 在 GameObject 菜单中增加功能：  
A. 生成 UGUI 组件代码  
B. 将代码绑定到组件上，相同则覆盖  
![image](https://github.com/user-attachments/assets/1adf52ac-4d4f-4362-bb4f-d5a97dd133c0)
  
## 操作步骤  
### 1、在 UI 组件上挂载 UICodeAttacher：  
UICodeAttacher：挂在 gameObject 上  
  -- AttachingUI: 拖入需要生成代码的 UI 组件  
  -- OverrideName: 用于代码中重命名  
![image](https://github.com/user-attachments/assets/53bb8cb7-f0d2-49fb-845f-1d8680191c3e)

### 2、生成代码
在 UI prefab 根节点上，右键-【UI Code Gen 生成UI代码】，就会生成代码：  
![image](https://github.com/user-attachments/assets/5eddeeaf-69cf-4739-846f-6a49b76ef68b)  

代码生成路径，配置在 UICodeGeneratorParam 中：  
![image](https://github.com/user-attachments/assets/a16defe5-ca54-40da-a927-ecc464d2a954)  
![image](https://github.com/user-attachments/assets/c4154d09-6f22-4325-8bc2-fae8e75c2277)  



### 3、挂载代码  
生成后代码后，在 UI prefab 根节点上，右键-【UI Code Attach 挂载UI代码】，就会挂载上代码  
![image](https://github.com/user-attachments/assets/402bba2c-1f05-40cf-8207-d5cf1c794aab)


### 4、引用丢失与恢复
脚本中已存入和根节点的相对关系，如果引用丢失，点击代码组件 reset 即可恢复引用  
![image](https://github.com/user-attachments/assets/7441b273-0e80-4e4c-8181-0dae9cee267d)  
![image](https://github.com/user-attachments/assets/02f9de52-78b5-4bed-b614-b2ddde908c9b)  



