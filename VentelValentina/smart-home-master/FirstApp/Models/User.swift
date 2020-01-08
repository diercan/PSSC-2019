//
//  TabelaUser.swift
//  FirstApp
//
//  Created by Valentina Vențel on 10/05/2019.
//  Copyright © 2019 Valentina Vențel. All rights reserved.
//

import UIKit
import Foundation

class User: NSObject {

    var userID: String?
    var password: String?
    var name: String?
    var puk: String?
    
    var fullName: String {
        return "\(userID!) \(name!)"
    }
    
    override init() {}
    
    init(userID: String, password: String, name: String, puk: String) {
        self.userID = userID
        self.password = password
        self.name = name
        self.puk = puk
    }
    
    
    
    override var description: String {
        return "\(userID!), \(puk!)"
    }

}
