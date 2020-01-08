//
//  ViewController.swift
//  FirstApp
//
//  Created by Valentina Vențel on 05/04/2019.
//  Copyright © 2019 Valentina Vențel. All rights reserved.
//

import UIKit
import Foundation


class LoginViewController: UIViewController, UserModelProtocol {
    
    @IBOutlet weak var pukText: UITextField!
    @IBOutlet weak var userText: UITextField!
    @IBOutlet weak var loginButton: UIButton!
    
    var feedItems: NSArray = NSArray()
    var selectedUser: User = User()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
//        let homeModel = HomeModel()
//        homeModel.delegate = self
//        homeModel.downloadItems()
        NotificationCenter.default.addObserver(self,
                                               selector: #selector(keyboardWillShow),
                                               name: UIResponder.keyboardWillShowNotification,
                                                object: nil)
        NotificationCenter.default.addObserver(self,
                                               selector: #selector(keyboardWillHide),
                                               name: UIResponder.keyboardWillHideNotification,
                                               object: nil)
    }
    
    override func viewDidAppear(_ animated: Bool) {
        super.viewDidAppear(animated)
        
    }
    
    @IBAction func loginTap(_ sender: UIButton) {
        guard let user = userText.text,
            let puk = pukText.text else {
                return
        }
        // MARK: Test
//        if user.lowercased() == "test" {
//             self.performSegue(withIdentifier: "mainSegue", sender: nil)
//        }
        
        HTTPClient.login(user: user,
                         puk: puk) { (result, err) in
                            if err == nil {
                                for dict in result as! [[String: Any]] {
                                    let nume = dict["UserID"] as! String
                                    if nume == user {
                                        DispatchQueue.main.async {
                                            self.performSegue(withIdentifier: "mainSegue", sender: nil)
                                        }
                                    }
                                }
                            } else {
                                // ...
                            }
        }
    }
        
    @objc func keyboardWillShow(notification: NSNotification) {
        guard let userInfo = notification.userInfo,
            let keyboardSize = userInfo[UIResponder.keyboardFrameEndUserInfoKey] as? NSValue else {
            return
        }
        let keyboardFrame = keyboardSize.cgRectValue
        if view.frame.origin.y <= 100 {
            view.frame.origin.y -= keyboardFrame.height
        }
    }
    @objc func keyboardWillHide(notification: NSNotification) {
        if view.frame.origin.y != 0 {
            view.frame.origin.y = 0
        }
    }
    
    
    //TODO: remove this shit
    func itemsDownloaded(items: NSArray) {
        feedItems = items
    }
    
    func returnUser(feedItems: NSArray) -> User {
        var index = 0
        let user = User()
        
        while index < feedItems.count {
            let user: User = feedItems[index] as! User
            if (user.userID == userText.text) && (user.puk == pukText.text) {
                return user
            }
            index += 1
        }
        return user
    }

}

