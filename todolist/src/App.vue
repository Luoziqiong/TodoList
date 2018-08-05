<template>
  <div id="app">
    <el-container>
      <el-header height="50px">
        <el-row :gutter="20">
          <el-col :span="3" class="logo">TODOLIST</el-col>
          <el-col :span="2" class="user" v-if="isLogin">
            <el-dropdown placement="bottom" size="medium" @command="handleCommand">
              <span class="el-dropdown-link">
                 <img src="./assets/logo.png" alt="userImg" class="userImg">
              </span>
              <el-dropdown-menu slot="dropdown">
                <el-dropdown-item command="updatePswd">修改密码</el-dropdown-item>
                <el-dropdown-item command="logout">退出登录</el-dropdown-item>
              </el-dropdown-menu>
            </el-dropdown>
          </el-col>
        </el-row>
      </el-header>
      <el-main>
          <router-view/>
      </el-main>
    </el-container>
    <!-- 弹窗相关 -->
        <el-dialog title="修改密码" :visible.sync="dialogVisible" width="400px" >
              <el-form ref="updatePswdForm" :model="updatePswdForm" label-width="80px" size="mini">
                <el-form-item label="旧密码">
                  <el-input v-model="updatePswdForm.oldPswd"></el-input>
                </el-form-item>
                <el-form-item label="新密码">
                  <el-input v-model="updatePswdForm.newPswd"></el-input>
                </el-form-item>
                <el-form-item label="确认密码">
                  <el-input v-model="updatePswdForm.newPswd2"></el-input>
                </el-form-item>
            </el-form>
            <div slot="footer" class="dialog-footer">
              <el-button type="primary" @click="submitUpdate" size="mini">确 定</el-button>
              <el-button @click="cancel" size="mini">取 消</el-button>
            </div>
        </el-dialog>
  </div>
</template>

<script>
export default {
  name: "App",
  data() {
    return {
      isLogin: true,
      dialogVisible:false,
      updatePswdForm:{}
    };
  },
  methods: {
    handleCommand(cmd) {
      switch (cmd) {
        case "logout":
          this.logout();
          break;
        case "updatePswd":
          this.updatePswd();
          break;
      }
    },
    logout() {
      this.$router.push({ path: "/login" });
    },
    updatePswd() {
      this.dialogVisible = true;
    },
    submitUpdate(){
      this.dialogVisible = false;

    },
    cancel(){
      this.dialogVisible = false;
    }
  }
};
</script>

<style>
body {
  margin: 0;
  padding: 0;
  background: #f0f3f6;
}

#app {
  font-family: "Avenir", Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #333;
}
.el-header {
  width: 101%;
  padding: 0;
  background: #0758ad;
  color: #fff;
}
.el-main {
  padding: 0;
}
.el-row {
  line-height: 50px;
}
.logo {
  font-size: 28px;
  margin-left: 15px;
}
.user {
  float: right;
}
.userImg {
  float: right;
  width: 40px;
  height: 40px;
  margin-top: 5px;
  margin-right: 25px;
  border-radius: 50%;
}
.el-dropdown-menu {
  top: 10px;
}
.el-dropdown-link {
  height: 50px;
}
</style>
